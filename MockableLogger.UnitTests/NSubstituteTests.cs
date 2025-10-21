using System;
using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace MockableLogger.UnitTests;

public class NSubstituteTests
{
    private readonly IMockableLogger _logger;
    private readonly TestClass _sut;
    private readonly TestLoggerAdapter<TestClass> _adapter;

    public NSubstituteTests()
    {
        _logger = Substitute.For<IMockableLogger>();
        _adapter = new TestLoggerAdapter<TestClass>(_logger);
        _sut = new TestClass(_adapter);
    }

    // Trace tests
    [Fact]
    public void DoLogTrace_LogsTrace()
    {
        _sut.DoLogTrace();
        _logger.Received(1).LogTrace("Trace logged");
    }

    [Fact]
    public void DoLogTraceWithArgs_LogsTraceWithArgs()
    {
        _sut.DoLogTraceWithArgs();
        _logger.Received(1).LogTrace("Trace with {Name}", Arg.Is<object[]>(args => args[0].ToString() == "value"));
    }

    [Fact]
    public void DoLogTraceWithException_LogsTraceWithException()
    {
        _sut.DoLogTraceWithException();
        _logger.Received(1).LogTrace(Arg.Any<Exception>(), "Trace with exception");
    }

    [Fact]
    public void DoLogTraceWithEventId_LogsTraceWithEventId()
    {
        _sut.DoLogTraceWithEventId();
        _logger.Received(1).LogTrace(Arg.Is<EventId>(e => e.Id == 1 && e.Name == "TraceEvent"), "Trace with EventId");
    }

    [Fact]
    public void DoLogTraceWithEventIdAndException_LogsTraceWithEventIdAndException()
    {
        _sut.DoLogTraceWithEventIdAndException();
        _logger.Received(1).LogTrace(Arg.Is<EventId>(e => e.Id == 1 && e.Name == "TraceEvent"), Arg.Any<Exception>(), "Trace with EventId and exception");
    }

    // Debug tests
    [Fact]
    public void DoLogDebug_LogsDebug()
    {
        _sut.DoLogDebug();
        _logger.Received(1).LogDebug("Debug logged");
    }

    [Fact]
    public void DoLogDebugWithArgs_LogsDebugWithArgs()
    {
        _sut.DoLogDebugWithArgs();
        _logger.Received(1).LogDebug("Debug with {Name}", Arg.Is<object[]>(args => args[0].ToString() == "value"));
    }

    [Fact]
    public void DoLogDebugWithException_LogsDebugWithException()
    {
        _sut.DoLogDebugWithException();
        _logger.Received(1).LogDebug(Arg.Any<Exception>(), "Debug with exception");
    }

    [Fact]
    public void DoLogDebugWithEventId_LogsDebugWithEventId()
    {
        _sut.DoLogDebugWithEventId();
        _logger.Received(1).LogDebug(Arg.Is<EventId>(e => e.Id == 2 && e.Name == "DebugEvent"), "Debug with EventId");
    }

    [Fact]
    public void DoLogDebugWithEventIdAndException_LogsDebugWithEventIdAndException()
    {
        _sut.DoLogDebugWithEventIdAndException();
        _logger.Received(1).LogDebug(Arg.Is<EventId>(e => e.Id == 2 && e.Name == "DebugEvent"), Arg.Any<Exception>(), "Debug with EventId and exception");
    }

    // Information tests
    [Fact]
    public void DoLogInfo_LogsInformation()
    {
        _sut.DoLogInfo();
        _logger.Received(1).LogInformation("Information logged");
    }

    [Fact]
    public void DoLogInfoWithArgs_LogsInformationWithArgs()
    {
        _sut.DoLogInfoWithArgs();
        _logger.Received(1).LogInformation("Info with {Name}", Arg.Is<object[]>(args => args[0].ToString() == "value"));
    }

    [Fact]
    public void DoLogInfoWithException_LogsInformationWithException()
    {
        _sut.DoLogInfoWithException();
        _logger.Received(1).LogInformation(Arg.Any<Exception>(), "Info with exception");
    }

    [Fact]
    public void DoLogInfoWithEventId_LogsInformationWithEventId()
    {
        _sut.DoLogInfoWithEventId();
        _logger.Received(1).LogInformation(Arg.Is<EventId>(e => e.Id == 3 && e.Name == "InfoEvent"), "Info with EventId");
    }

    [Fact]
    public void DoLogInfoWithEventIdAndException_LogsInformationWithEventIdAndException()
    {
        _sut.DoLogInfoWithEventIdAndException();
        _logger.Received(1).LogInformation(Arg.Is<EventId>(e => e.Id == 3 && e.Name == "InfoEvent"), Arg.Any<Exception>(), "Info with EventId and exception");
    }

    // Warning tests
    [Fact]
    public void DoLogWarning_LogsWarning()
    {
        _sut.DoLogWarning();
        _logger.Received(1).LogWarning("Warning logged");
    }

    [Fact]
    public void DoLogWarningWithArgs_LogsWarningWithArgs()
    {
        _sut.DoLogWarningWithArgs();
        _logger.Received(1).LogWarning("Warning with {Name}", Arg.Is<object[]>(args => args[0].ToString() == "value"));
    }

    [Fact]
    public void DoLogWarningWithException_LogsWarningWithException()
    {
        _sut.DoLogWarningWithException();
        _logger.Received(1).LogWarning(Arg.Any<Exception>(), "Warning with exception");
    }

    [Fact]
    public void DoLogWarningWithEventId_LogsWarningWithEventId()
    {
        _sut.DoLogWarningWithEventId();
        _logger.Received(1).LogWarning(Arg.Is<EventId>(e => e.Id == 4 && e.Name == "WarningEvent"), "Warning with EventId");
    }

    [Fact]
    public void DoLogWarningWithEventIdAndException_LogsWarningWithEventIdAndException()
    {
        _sut.DoLogWarningWithEventIdAndException();
        _logger.Received(1).LogWarning(Arg.Is<EventId>(e => e.Id == 4 && e.Name == "WarningEvent"), Arg.Any<Exception>(), "Warning with EventId and exception");
    }

    // Error tests
    [Fact]
    public void DoLogError_LogsError()
    {
        _sut.DoLogError();
        _logger.Received(1).LogError("Error logged");
    }

    [Fact]
    public void DoLogErrorWithArgs_LogsErrorWithArgs()
    {
        _sut.DoLogErrorWithArgs();
        _logger.Received(1).LogError("Error with {Name}", Arg.Is<object[]>(args => args[0].ToString() == "value"));
    }

    [Fact]
    public void DoLogErrorWithException_LogsErrorWithException()
    {
        _sut.DoLogErrorWithException();
        _logger.Received(1).LogError(Arg.Any<Exception>(), "Error with exception");
    }

    [Fact]
    public void DoLogErrorWithEventId_LogsErrorWithEventId()
    {
        _sut.DoLogErrorWithEventId();
        _logger.Received(1).LogError(Arg.Is<EventId>(e => e.Id == 5 && e.Name == "ErrorEvent"), "Error with EventId");
    }

    [Fact]
    public void DoLogErrorWithEventIdAndException_LogsErrorWithEventIdAndException()
    {
        _sut.DoLogErrorWithEventIdAndException();
        _logger.Received(1).LogError(Arg.Is<EventId>(e => e.Id == 5 && e.Name == "ErrorEvent"), Arg.Any<Exception>(), "Error with EventId and exception");
    }

    // Critical tests
    [Fact]
    public void DoLogCritical_LogsCritical()
    {
        _sut.DoLogCritical();
        _logger.Received(1).LogCritical("Critical logged");
    }

    [Fact]
    public void DoLogCriticalWithArgs_LogsCriticalWithArgs()
    {
        _sut.DoLogCriticalWithArgs();
        _logger.Received(1).LogCritical("Critical with {Name}", Arg.Is<object[]>(args => args[0].ToString() == "value"));
    }

    [Fact]
    public void DoLogCriticalWithException_LogsCriticalWithException()
    {
        _sut.DoLogCriticalWithException();
        _logger.Received(1).LogCritical(Arg.Any<Exception>(), "Critical with exception");
    }

    [Fact]
    public void DoLogCriticalWithEventId_LogsCriticalWithEventId()
    {
        _sut.DoLogCriticalWithEventId();
        _logger.Received(1).LogCritical(Arg.Is<EventId>(e => e.Id == 6 && e.Name == "CriticalEvent"), "Critical with EventId");
    }

    [Fact]
    public void DoLogCriticalWithEventIdAndException_LogsCriticalWithEventIdAndException()
    {
        _sut.DoLogCriticalWithEventIdAndException();
        _logger.Received(1).LogCritical(Arg.Is<EventId>(e => e.Id == 6 && e.Name == "CriticalEvent"), Arg.Any<Exception>(), "Critical with EventId and exception");
    }

    [Fact]
    public void LogLevelNone_NSubstitute_DoesNotCallUnderlyingLogger()
    {
        _adapter.Log(LogLevel.None, new EventId(1), "Test", null, (s, e) => s);

        _logger.DidNotReceive().LogTrace(Arg.Any<string>(), Arg.Any<object[]>());
        _logger.DidNotReceive().LogDebug(Arg.Any<string>(), Arg.Any<object[]>());
        _logger.DidNotReceive().LogInformation(Arg.Any<string>(), Arg.Any<object[]>());
        _logger.DidNotReceive().LogWarning(Arg.Any<string>(), Arg.Any<object[]>());
        _logger.DidNotReceive().LogError(Arg.Any<string>(), Arg.Any<object[]>());
        _logger.DidNotReceive().LogCritical(Arg.Any<string>(), Arg.Any<object[]>());
    }

    [Fact]
    public void BeginScope_WithString_CallsUnderlyingLogger()
    {
        var scopeState = "TestScope";

        _adapter.BeginScope(scopeState);

        _logger.Received(1).BeginScope(scopeState);
    }

    [Fact]
    public void BeginScope_WithDictionary_CallsUnderlyingLogger()
    {
        var scopeState = new Dictionary<string, object>
        {
            ["UserId"] = 123,
            ["RequestId"] = "abc-123"
        };

        _adapter.BeginScope(scopeState);

        _logger.Received(1).BeginScope(scopeState);
    }

    [Fact]
    public void BeginScope_ReturnsDisposableFromUnderlyingLogger()
    {
        var expectedDisposable = Substitute.For<IDisposable>();
        _logger.BeginScope(Arg.Any<string>()).Returns(expectedDisposable);

        var result = _adapter.BeginScope("TestScope");

        result.Should().Be(expectedDisposable);
    }

    [Fact]
    public void BeginScope_CanBeDisposed()
    {
        var mockDisposable = Substitute.For<IDisposable>();
        _logger.BeginScope(Arg.Any<string>()).Returns(mockDisposable);

        using (var scope = _adapter.BeginScope("TestScope"))
        {
            scope.Should().NotBeNull();
        }

        mockDisposable.Received(1).Dispose();
    }

    [Fact]
    public void LogInformation_Callback_CapturesLogMessage()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        string? capturedMessage = null;

        mockLogger.LogInformation(Arg.Do<string>(msg => capturedMessage = msg));

        // Act
        sut.DoLogInfo();

        // Assert
        capturedMessage.Should().Be("Information logged");
    }

    [Fact]
    public void LogInformation_Callback_CapturesStructuredLoggingArgs()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        string? capturedMessage = null;
        object?[]? capturedArgs = null;

        mockLogger.LogInformation(
            Arg.Do<string>(msg => capturedMessage = msg),
            Arg.Do<object?[]>(args => capturedArgs = args)
        );

        // Act
        sut.DoLogInfoWithArgs();

        // Assert
        capturedMessage.Should().Be("Info with {Name}");
        capturedArgs.Should().NotBeNull();
        capturedArgs.Should().ContainSingle();
        capturedArgs![0].Should().Be("value");
    }

    [Fact]
    public void LogError_Callback_CapturesException()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        Exception? capturedException = null;
        string? capturedMessage = null;

        mockLogger.LogError(
            Arg.Do<Exception>(ex => capturedException = ex),
            Arg.Do<string>(msg => capturedMessage = msg)
        );

        // Act
        sut.DoLogErrorWithException();

        // Assert
        capturedException.Should().NotBeNull();
        capturedException!.Message.Should().Be("error");
        capturedMessage.Should().Be("Error with exception");
    }

    [Fact]
    public void LogWarning_Callback_CapturesEventId()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        EventId? capturedEventId = null;
        string? capturedMessage = null;

        mockLogger.LogWarning(
            Arg.Do<EventId>(eventId => capturedEventId = eventId),
            Arg.Do<string>(msg => capturedMessage = msg)
        );

        // Act
        sut.DoLogWarningWithEventId();

        // Assert
        capturedEventId.Should().NotBeNull();
        capturedEventId!.Value.Id.Should().Be(4);
        capturedEventId.Value.Name.Should().Be("WarningEvent");
        capturedMessage.Should().Be("Warning with EventId");
    }

    [Fact]
    public void LogCritical_Callback_CapturesAllParameters()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        EventId? capturedEventId = null;
        Exception? capturedException = null;
        string? capturedMessage = null;

        mockLogger.LogCritical(
            Arg.Do<EventId>(eventId => capturedEventId = eventId),
            Arg.Do<Exception>(ex => capturedException = ex),
            Arg.Do<string>(msg => capturedMessage = msg)
        );

        // Act
        sut.DoLogCriticalWithEventIdAndException();

        // Assert
        capturedEventId.Should().NotBeNull();
        capturedEventId!.Value.Id.Should().Be(6);
        capturedEventId.Value.Name.Should().Be("CriticalEvent");
        capturedException.Should().NotBeNull();
        capturedException!.Message.Should().Be("critical");
        capturedMessage.Should().Be("Critical with EventId and exception");
    }

    [Fact]
    public void LogDebug_Callback_CountsInvocations()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        var callCount = 0;

        mockLogger.LogDebug(Arg.Do<string>(_ => callCount++));

        // Act
        sut.DoLogDebug();
        sut.DoLogDebug();
        sut.DoLogDebug();

        // Assert
        callCount.Should().Be(3);
    }

    [Fact]
    public void MultipleLogLevels_Callback_DistinguishesLogLevels()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        var loggedMessages = new List<(string Level, string Message)>();

        mockLogger.LogInformation(Arg.Do<string>(msg =>
            loggedMessages.Add(("Info", msg))));
        mockLogger.LogWarning(Arg.Do<string>(msg =>
            loggedMessages.Add(("Warning", msg))));
        mockLogger.LogError(Arg.Do<string>(msg =>
            loggedMessages.Add(("Error", msg))));

        // Act
        sut.DoLogInfo();
        sut.DoLogWarning();
        sut.DoLogError();

        // Assert
        loggedMessages.Should().HaveCount(3);
        loggedMessages.Should().Contain(("Info", "Information logged"));
        loggedMessages.Should().Contain(("Warning", "Warning logged"));
        loggedMessages.Should().Contain(("Error", "Error logged"));
    }

    [Fact]
    public void Callback_CanPerformSideEffects()
    {
        // Arrange
        var mockLogger = Substitute.For<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger);
        var sut = new TestClass(logger);
        var sideEffectTriggered = false;

        var mockDisposable = Substitute.For<IDisposable>();
        mockLogger.BeginScope(Arg.Any<string>())
            .Returns(_ =>
            {
                sideEffectTriggered = true;
                return mockDisposable; // or whatever return type ITestLogger.LogError has
            });

        // Act
        sut.DoScopedLogInformation();

        // Assert
        sideEffectTriggered.Should().BeTrue();
    }
}