using System;
using Microsoft.Extensions.Logging;

namespace MockableLogger;

public interface IMockableLogger
{
    // Debug
    void LogDebug(EventId eventId, Exception? exception, string? message, params object?[] args);
    void LogDebug(EventId eventId, string? message, params object?[] args);
    void LogDebug(Exception? exception, string? message, params object?[] args);
    void LogDebug(string? message, params object?[] args);

    // Trace
    void LogTrace(EventId eventId, Exception? exception, string? message, params object?[] args);
    void LogTrace(EventId eventId, string? message, params object?[] args);
    void LogTrace(Exception? exception, string? message, params object?[] args);
    void LogTrace(string? message, params object?[] args);

    // Information
    void LogInformation(EventId eventId, Exception? exception, string? message, params object?[] args);
    void LogInformation(EventId eventId, string? message, params object?[] args);
    void LogInformation(Exception? exception, string? message, params object?[] args);
    void LogInformation(string? message, params object?[] args);

    // Warning
    void LogWarning(EventId eventId, Exception? exception, string? message, params object?[] args);
    void LogWarning(EventId eventId, string? message, params object?[] args);
    void LogWarning(Exception? exception, string? message, params object?[] args);
    void LogWarning(string? message, params object?[] args);

    // Error
    void LogError(EventId eventId, Exception? exception, string? message, params object?[] args);
    void LogError(EventId eventId, string? message, params object?[] args);
    void LogError(Exception? exception, string? message, params object?[] args);
    void LogError(string? message, params object?[] args);

    // Critical
    void LogCritical(EventId eventId, Exception? exception, string? message, params object?[] args);
    void LogCritical(EventId eventId, string? message, params object?[] args);
    void LogCritical(Exception? exception, string? message, params object?[] args);
    void LogCritical(string? message, params object?[] args);

    IDisposable? BeginScope<TState>(TState state) where TState : notnull;
    bool IsEnabled(LogLevel logLevel);
}
