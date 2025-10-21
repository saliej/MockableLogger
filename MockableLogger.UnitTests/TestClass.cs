using System;
using Microsoft.Extensions.Logging;

namespace MockableLogger.UnitTests;

public class TestClass
{
    private readonly ILogger _logger;

    public TestClass(ILogger<TestClass> logger)
    {
        _logger = logger;
    }

    // Trace methods
    public void DoLogTrace() => _logger.LogTrace("Trace logged");
    public void DoLogTraceWithArgs() => _logger.LogTrace("Trace with {Name}", "value");
    public void DoLogTraceWithException() => _logger.LogTrace(new Exception("trace"), "Trace with exception");
    public void DoLogTraceWithEventId() => _logger.LogTrace(new EventId(1, "TraceEvent"), "Trace with EventId");
    public void DoLogTraceWithEventIdAndException() => _logger.LogTrace(new EventId(1, "TraceEvent"), new Exception("trace"), "Trace with EventId and exception");

    // Debug methods
    public void DoLogDebug() => _logger.LogDebug("Debug logged");
    public void DoLogDebugWithArgs() => _logger.LogDebug("Debug with {Name}", "value");
    public void DoLogDebugWithException() => _logger.LogDebug(new Exception("debug"), "Debug with exception");
    public void DoLogDebugWithEventId() => _logger.LogDebug(new EventId(2, "DebugEvent"), "Debug with EventId");
    public void DoLogDebugWithEventIdAndException() => _logger.LogDebug(new EventId(2, "DebugEvent"), new Exception("debug"), "Debug with EventId and exception");

    // Information methods
    public void DoLogInfo() => _logger.LogInformation("Information logged");
    public void DoLogInfoWithArgs() => _logger.LogInformation("Info with {Name}", "value");
    public void DoLogInfoWithException() => _logger.LogInformation(new Exception("info"), "Info with exception");
    public void DoLogInfoWithEventId() => _logger.LogInformation(new EventId(3, "InfoEvent"), "Info with EventId");
    public void DoLogInfoWithEventIdAndException() => _logger.LogInformation(new EventId(3, "InfoEvent"), new Exception("info"), "Info with EventId and exception");

    // Warning methods
    public void DoLogWarning() => _logger.LogWarning("Warning logged");
    public void DoLogWarningWithArgs() => _logger.LogWarning("Warning with {Name}", "value");
    public void DoLogWarningWithException() => _logger.LogWarning(new Exception("warning"), "Warning with exception");
    public void DoLogWarningWithEventId() => _logger.LogWarning(new EventId(4, "WarningEvent"), "Warning with EventId");
    public void DoLogWarningWithEventIdAndException() => _logger.LogWarning(new EventId(4, "WarningEvent"), new Exception("warning"), "Warning with EventId and exception");

    // Error methods
    public void DoLogError() => _logger.LogError("Error logged");
    public void DoLogErrorWithArgs() => _logger.LogError("Error with {Name}", "value");
    public void DoLogErrorWithException() => _logger.LogError(new Exception("error"), "Error with exception");
    public void DoLogErrorWithEventId() => _logger.LogError(new EventId(5, "ErrorEvent"), "Error with EventId");
    public void DoLogErrorWithEventIdAndException() => _logger.LogError(new EventId(5, "ErrorEvent"), new Exception("error"), "Error with EventId and exception");

    // Critical methods
    public void DoLogCritical() => _logger.LogCritical("Critical logged");
    public void DoLogCriticalWithArgs() => _logger.LogCritical("Critical with {Name}", "value");
    public void DoLogCriticalWithException() => _logger.LogCritical(new Exception("critical"), "Critical with exception");
    public void DoLogCriticalWithEventId() => _logger.LogCritical(new EventId(6, "CriticalEvent"), "Critical with EventId");
    public void DoLogCriticalWithEventIdAndException() => _logger.LogCritical(new EventId(6, "CriticalEvent"), new Exception("critical"), "Critical with EventId and exception");

    // Scoped logs
    public void DoScopedLogInformation()
    {
        using (_logger.BeginScope("Scope1"))
        {
            _logger.LogInformation("Information within scope");
        }
    }
}