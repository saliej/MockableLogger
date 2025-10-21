using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace MockableLogger;

public class TestLoggerAdapter<T>(IMockableLogger testableLogger) : ILogger<T>
{
    // Cached reflection info for FormattedLogValues
    private static readonly FieldInfo? OriginalMessageField;
    private static readonly FieldInfo? ValuesField;

    static TestLoggerAdapter()
    {
        // Cache reflection lookups for FormattedLogValues
        var formattedLogValuesType = Type.GetType("Microsoft.Extensions.Logging.FormattedLogValues, Microsoft.Extensions.Logging.Abstractions");
        if (formattedLogValuesType != null)
        {
            OriginalMessageField = formattedLogValuesType.GetField("_originalMessage", BindingFlags.Instance | BindingFlags.NonPublic);
            ValuesField = formattedLogValuesType.GetField("_values", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }

    public bool IsEnabled(LogLevel logLevel) => testableLogger.IsEnabled(logLevel);

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull =>
        testableLogger.BeginScope(state);

    // Main logging method
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        // Handle LogLevel.None - don't log anything
        if (logLevel == LogLevel.None)
            return;

        // Try to extract structured logging info from various state types
        if (TryExtractStructuredInfo(state, out var message, out var args))
        {
            RouteToMethod(logLevel, eventId, exception, message, args);
            return;
        }

        // Fallback: use the formatter
        var formattedMessage = formatter(state, exception);
        RouteToMethod(logLevel, eventId, exception, formattedMessage, null);
    }

    private bool TryExtractStructuredInfo<TState>(TState state, out string? message, out object?[]? args)
    {
        message = null;
        args = null;

        if (state == null)
            return false;

        var stateType = state.GetType();

        // Handle FormattedLogValues (most common case)
        if (stateType.Name == "FormattedLogValues" && OriginalMessageField != null && ValuesField != null)
        {
            try
            {
                message = OriginalMessageField.GetValue(state) as string;
                args = ValuesField.GetValue(state) as object?[];
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Handle plain string
        if (state is string str)
        {
            message = str;
            args = null;
            return true;
        }

        // Handle IReadOnlyList<KeyValuePair<string, object>> (structured logging)
        if (state is IReadOnlyList<KeyValuePair<string, object>> kvpList)
        {
            // The last item is typically the original format string with key "{OriginalFormat}"
            for (int i = kvpList.Count - 1; i >= 0; i--)
            {
                if (kvpList[i].Key == "{OriginalFormat}")
                {
                    message = kvpList[i].Value?.ToString();
                    
                    // Extract values (excluding the OriginalFormat entry)
                    var valueList = new List<object?>();
                    for (int j = 0; j < kvpList.Count; j++)
                    {
                        if (j != i)
                            valueList.Add(kvpList[j].Value);
                    }
                    args = valueList.ToArray();
                    return true;
                }
            }
        }

        // Handle IEnumerable<KeyValuePair<string, object>>
        if (state is IEnumerable<KeyValuePair<string, object>> kvpEnumerable)
        {
            var list = new List<KeyValuePair<string, object>>(kvpEnumerable);
            var originalFormat = list.Find(kvp => kvp.Key == "{OriginalFormat}");
            if (!originalFormat.Equals(default(KeyValuePair<string, object>)))
            {
                message = originalFormat.Value?.ToString();
                var valueList = new List<object?>();
                foreach (var kvp in list)
                {
                    if (kvp.Key != "{OriginalFormat}")
                        valueList.Add(kvp.Value);
                }
                args = valueList.ToArray();
                return true;
            }
        }

        return false;
    }

    private void RouteToMethod(LogLevel logLevel, EventId eventId, Exception? exception, string? message, object?[]? args)
    {
        // Determine which overload to call based on parameters
        // EventId is considered "present" if it has a non-zero Id OR a non-empty Name
        var hasEventId = eventId.Id != 0 || !string.IsNullOrEmpty(eventId.Name);
        var hasException = exception != null;

        switch (logLevel)
        {
            case LogLevel.Debug:
                if (hasEventId && hasException)
                    testableLogger.LogDebug(eventId, exception, message, args ?? []);
                else if (hasEventId)
                    testableLogger.LogDebug(eventId, message, args ?? []);
                else if (hasException)
                    testableLogger.LogDebug(exception, message, args ?? []);
                else
                    testableLogger.LogDebug(message, args ?? []);
                break;

            case LogLevel.Trace:
                if (hasEventId && hasException)
                    testableLogger.LogTrace(eventId, exception, message, args ?? []);
                else if (hasEventId)
                    testableLogger.LogTrace(eventId, message, args ?? []);
                else if (hasException)
                    testableLogger.LogTrace(exception, message, args ?? []);
                else
                    testableLogger.LogTrace(message, args ?? []);
                break;

            case LogLevel.Information:
                if (hasEventId && hasException)
                    testableLogger.LogInformation(eventId, exception, message, args ?? []);
                else if (hasEventId)
                    testableLogger.LogInformation(eventId, message, args ?? []);
                else if (hasException)
                    testableLogger.LogInformation(exception, message, args ?? []);
                else
                    testableLogger.LogInformation(message, args ?? []);
                break;

            case LogLevel.Warning:
                if (hasEventId && hasException)
                    testableLogger.LogWarning(eventId, exception, message, args ?? []);
                else if (hasEventId)
                    testableLogger.LogWarning(eventId, message, args ?? []);
                else if (hasException)
                    testableLogger.LogWarning(exception, message, args ?? []);
                else
                    testableLogger.LogWarning(message, args ?? []);
                break;

            case LogLevel.Error:
                if (hasEventId && hasException)
                    testableLogger.LogError(eventId, exception, message, args ?? []);
                else if (hasEventId)
                    testableLogger.LogError(eventId, message, args ?? []);
                else if (hasException)
                    testableLogger.LogError(exception, message, args ?? []);
                else
                    testableLogger.LogError(message, args ?? []);
                break;

            case LogLevel.Critical:
                if (hasEventId && hasException)
                    testableLogger.LogCritical(eventId, exception, message, args ?? []);
                else if (hasEventId)
                    testableLogger.LogCritical(eventId, message, args ?? []);
                else if (hasException)
                    testableLogger.LogCritical(exception, message, args ?? []);
                else
                    testableLogger.LogCritical(message, args ?? []);
                break;

            case LogLevel.None:
                // Already handled in Log method, but included here for completeness
                break;
        }
    }
}