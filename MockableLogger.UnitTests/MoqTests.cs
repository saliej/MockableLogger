using System;
using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace MockableLogger.UnitTests;

public class MoqTests
{
    private readonly Mock<IMockableLogger> _logger;
    private readonly TestLoggerAdapter<TestClass> _adapter;
    private readonly TestClass _sut;

    public MoqTests()
    {
        _logger = new Mock<IMockableLogger>();
        _adapter = new TestLoggerAdapter<TestClass>(_logger.Object);
        _sut = new TestClass(_adapter);
    }

    // Trace tests
    [Fact]
    public void DoLogTrace_LogsTrace()
    {
        _sut.DoLogTrace();
        _logger.Verify(l => l.LogTrace("Trace logged"), Times.Once);
    }

    [Fact]
    public void DoLogTraceWithArgs_LogsTraceWithArgs()
    {
        _sut.DoLogTraceWithArgs();
        _logger.Verify(l => l.LogTrace("Trace with {Name}", It.Is<object[]>(args => args[0].ToString() == "value")), Times.Once);
    }

    [Fact]
    public void DoLogTraceWithException_LogsTraceWithException()
    {
        _sut.DoLogTraceWithException();
        _logger.Verify(l => l.LogTrace(It.IsAny<Exception>(), "Trace with exception"), Times.Once);
    }

    [Fact]
    public void DoLogTraceWithEventId_LogsTraceWithEventId()
    {
        _sut.DoLogTraceWithEventId();
        _logger.Verify(l => l.LogTrace(It.Is<EventId>(e => e.Id == 1 && e.Name == "TraceEvent"), "Trace with EventId"), Times.Once);
    }

    [Fact]
    public void DoLogTraceWithEventIdAndException_LogsTraceWithEventIdAndException()
    {
        _sut.DoLogTraceWithEventIdAndException();
        _logger.Verify(l => l.LogTrace(It.Is<EventId>(e => e.Id == 1 && e.Name == "TraceEvent"), It.IsAny<Exception>(), "Trace with EventId and exception"), Times.Once);
    }

    // Debug tests
    [Fact]
    public void DoLogDebug_LogsDebug()
    {
        _sut.DoLogDebug();
        _logger.Verify(l => l.LogDebug("Debug logged"), Times.Once);
    }

    [Fact]
    public void DoLogDebugWithArgs_LogsDebugWithArgs()
    {
        _sut.DoLogDebugWithArgs();
        _logger.Verify(l => l.LogDebug("Debug with {Name}", It.Is<object[]>(args => args[0].ToString() == "value")), Times.Once);
    }

    [Fact]
    public void DoLogDebugWithException_LogsDebugWithException()
    {
        _sut.DoLogDebugWithException();
        _logger.Verify(l => l.LogDebug(It.IsAny<Exception>(), "Debug with exception"), Times.Once);
    }

    [Fact]
    public void DoLogDebugWithEventId_LogsDebugWithEventId()
    {
        _sut.DoLogDebugWithEventId();
        _logger.Verify(l => l.LogDebug(It.Is<EventId>(e => e.Id == 2 && e.Name == "DebugEvent"), "Debug with EventId"), Times.Once);
    }

    [Fact]
    public void DoLogDebugWithEventIdAndException_LogsDebugWithEventIdAndException()
    {
        _sut.DoLogDebugWithEventIdAndException();
        _logger.Verify(l => l.LogDebug(It.Is<EventId>(e => e.Id == 2 && e.Name == "DebugEvent"), It.IsAny<Exception>(), "Debug with EventId and exception"), Times.Once);
    }

    // Information tests
    [Fact]
    public void DoLogInfo_LogsInformation()
    {
        _sut.DoLogInfo();
        _logger.Verify(l => l.LogInformation("Information logged"), Times.Once);
    }

    [Fact]
    public void DoLogInfoWithArgs_LogsInformationWithArgs()
    {
        _sut.DoLogInfoWithArgs();
        _logger.Verify(l => l.LogInformation("Info with {Name}", It.Is<object[]>(args => args[0].ToString() == "value")), Times.Once);
    }

    [Fact]
    public void DoLogInfoWithException_LogsInformationWithException()
    {
        _sut.DoLogInfoWithException();
        _logger.Verify(l => l.LogInformation(It.IsAny<Exception>(), "Info with exception"), Times.Once);
    }

    [Fact]
    public void DoLogInfoWithEventId_LogsInformationWithEventId()
    {
        _sut.DoLogInfoWithEventId();
        _logger.Verify(l => l.LogInformation(It.Is<EventId>(e => e.Id == 3 && e.Name == "InfoEvent"), "Info with EventId"), Times.Once);
    }

    [Fact]
    public void DoLogInfoWithEventIdAndException_LogsInformationWithEventIdAndException()
    {
        _sut.DoLogInfoWithEventIdAndException();
        _logger.Verify(l => l.LogInformation(It.Is<EventId>(e => e.Id == 3 && e.Name == "InfoEvent"), It.IsAny<Exception>(), "Info with EventId and exception"), Times.Once);
    }

    // Warning tests
    [Fact]
    public void DoLogWarning_LogsWarning()
    {
        _sut.DoLogWarning();
        _logger.Verify(l => l.LogWarning("Warning logged"), Times.Once);
    }

    [Fact]
    public void DoLogWarningWithArgs_LogsWarningWithArgs()
    {
        _sut.DoLogWarningWithArgs();
        _logger.Verify(l => l.LogWarning("Warning with {Name}", It.Is<object[]>(args => args[0].ToString() == "value")), Times.Once);
    }

    [Fact]
    public void DoLogWarningWithException_LogsWarningWithException()
    {
        _sut.DoLogWarningWithException();
        _logger.Verify(l => l.LogWarning(It.IsAny<Exception>(), "Warning with exception"), Times.Once);
    }

    [Fact]
    public void DoLogWarningWithEventId_LogsWarningWithEventId()
    {
        _sut.DoLogWarningWithEventId();
        _logger.Verify(l => l.LogWarning(It.Is<EventId>(e => e.Id == 4 && e.Name == "WarningEvent"), "Warning with EventId"), Times.Once);
    }

    [Fact]
    public void DoLogWarningWithEventIdAndException_LogsWarningWithEventIdAndException()
    {
        _sut.DoLogWarningWithEventIdAndException();
        _logger.Verify(l => l.LogWarning(It.Is<EventId>(e => e.Id == 4 && e.Name == "WarningEvent"), It.IsAny<Exception>(), "Warning with EventId and exception"), Times.Once);
    }

    // Error tests
    [Fact]
    public void DoLogError_LogsError()
    {
        _sut.DoLogError();
        _logger.Verify(l => l.LogError("Error logged"), Times.Once);
    }

    [Fact]
    public void DoLogErrorWithArgs_LogsErrorWithArgs()
    {
        _sut.DoLogErrorWithArgs();
        _logger.Verify(l => l.LogError("Error with {Name}", It.Is<object[]>(args => args[0].ToString() == "value")), Times.Once);
    }

    [Fact]
    public void DoLogErrorWithException_LogsErrorWithException()
    {
        _sut.DoLogErrorWithException();
        _logger.Verify(l => l.LogError(It.IsAny<Exception>(), "Error with exception"), Times.Once);
    }

    [Fact]
    public void DoLogErrorWithEventId_LogsErrorWithEventId()
    {
        _sut.DoLogErrorWithEventId();
        _logger.Verify(l => l.LogError(It.Is<EventId>(e => e.Id == 5 && e.Name == "ErrorEvent"), "Error with EventId"), Times.Once);
    }

    [Fact]
    public void DoLogErrorWithEventIdAndException_LogsErrorWithEventIdAndException()
    {
        _sut.DoLogErrorWithEventIdAndException();
        _logger.Verify(l => l.LogError(It.Is<EventId>(e => e.Id == 5 && e.Name == "ErrorEvent"), It.IsAny<Exception>(), "Error with EventId and exception"), Times.Once);
    }

    // Critical tests
    [Fact]
    public void DoLogCritical_LogsCritical()
    {
        _sut.DoLogCritical();
        _logger.Verify(l => l.LogCritical("Critical logged"), Times.Once);
    }

    [Fact]
    public void DoLogCriticalWithArgs_LogsCriticalWithArgs()
    {
        _sut.DoLogCriticalWithArgs();
        _logger.Verify(l => l.LogCritical("Critical with {Name}", It.Is<object[]>(args => args[0].ToString() == "value")), Times.Once);
    }

    [Fact]
    public void DoLogCriticalWithException_LogsCriticalWithException()
    {
        _sut.DoLogCriticalWithException();
        _logger.Verify(l => l.LogCritical(It.IsAny<Exception>(), "Critical with exception"), Times.Once);
    }

    [Fact]
    public void DoLogCriticalWithEventId_LogsCriticalWithEventId()
    {
        _sut.DoLogCriticalWithEventId();
        _logger.Verify(l => l.LogCritical(It.Is<EventId>(e => e.Id == 6 && e.Name == "CriticalEvent"), "Critical with EventId"), Times.Once);
    }

    [Fact]
    public void DoLogCriticalWithEventIdAndException_LogsCriticalWithEventIdAndException()
    {
        _sut.DoLogCriticalWithEventIdAndException();
        _logger.Verify(l => l.LogCritical(It.Is<EventId>(e => e.Id == 6 && e.Name == "CriticalEvent"), It.IsAny<Exception>(), "Critical with EventId and exception"), Times.Once);
    }

    [Fact]
    public void LogLevelNone_Moq_DoesNotCallUnderlyingLogger()
    {
        _adapter.Log(LogLevel.None, new EventId(1), "Test", null, (s, e) => s);

        _logger.Verify(l => l.LogTrace(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        _logger.Verify(l => l.LogDebug(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        _logger.Verify(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        _logger.Verify(l => l.LogWarning(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        _logger.Verify(l => l.LogError(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        _logger.Verify(l => l.LogCritical(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
    }

    [Fact]
    public void BeginScope_WithString_CallsUnderlyingLogger()
    {
        var scopeState = "TestScope";

        _adapter.BeginScope(scopeState);

        _logger.Verify(l => l.BeginScope(scopeState), Times.Once);
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

        _logger.Verify(l => l.BeginScope(scopeState), Times.Once);
    }

    [Fact]
    public void BeginScope_ReturnsDisposableFromUnderlyingLogger()
    {
        var expectedDisposable = new Mock<IDisposable>().Object;
        _logger.Setup(l => l.BeginScope(It.IsAny<string>())).Returns(expectedDisposable);

        var result = _adapter.BeginScope("TestScope");

        result.Should().Be(expectedDisposable);
    }

    [Fact]
    public void BeginScope_CanBeDisposed()
    {
        var mockDisposable = new Mock<IDisposable>();
        _logger.Setup(l => l.BeginScope(It.IsAny<string>())).Returns(mockDisposable.Object);

        using (var scope = _adapter.BeginScope("TestScope"))
        {
            scope.Should().NotBeNull();
        }
                
        mockDisposable.Verify(d => d.Dispose(), Times.Once);
    }

    [Fact]
    public void LogInformation_Callback_CapturesLogMessage()
    {
        // Arrange
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        string? capturedMessage = null;

        mockLogger.Setup(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<string, object?[]>((msg, args) => capturedMessage = msg);

        // Act
        sut.DoLogInfo();

        // Assert
        capturedMessage.Should().Be("Information logged");
    }

    [Fact]
    public void LogInformation_Callback_CapturesStructuredLoggingArgs()
    {
        // Arrange
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        string? capturedMessage = null;
        object?[]? capturedArgs = null;

        mockLogger.Setup(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<string, object?[]>((msg, args) =>
            {
                capturedMessage = msg;
                capturedArgs = args;
            });

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
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        Exception? capturedException = null;
        string? capturedMessage = null;

        mockLogger.Setup(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<Exception, string, object?[]>((ex, msg, args) =>
            {
                capturedException = ex;
                capturedMessage = msg;
            });

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
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        EventId? capturedEventId = null;
        string? capturedMessage = null;

        mockLogger.Setup(l => l.LogWarning(It.IsAny<EventId>(), It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<EventId, string, object?[]>((eventId, msg, args) =>
            {
                capturedEventId = eventId;
                capturedMessage = msg;
            });

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
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        EventId? capturedEventId = null;
        Exception? capturedException = null;
        string? capturedMessage = null;

        mockLogger.Setup(l => l.LogCritical(
                It.IsAny<EventId>(),
                It.IsAny<Exception>(),
                It.IsAny<string>(),
                It.IsAny<object?[]>()))
            .Callback<EventId, Exception, string, object?[]>((eventId, ex, msg, args) =>
            {
                capturedEventId = eventId;
                capturedException = ex;
                capturedMessage = msg;
            });

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
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        var callCount = 0;

        mockLogger.Setup(l => l.LogDebug(It.IsAny<string?>(), It.IsAny<object?[]>()))
            .Callback<string, object?[]>((msg, args) => callCount++);

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
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        var loggedMessages = new List<(string Level, string Message)>();

        mockLogger.Setup(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<string, object?[]>((msg, args) => loggedMessages.Add(("Info", msg)));
        mockLogger.Setup(l => l.LogWarning(It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<string, object?[]>((msg, args) => loggedMessages.Add(("Warning", msg)));
        mockLogger.Setup(l => l.LogError(It.IsAny<string>(), It.IsAny<object?[]>()))
            .Callback<string, object?[]>((msg, args) => loggedMessages.Add(("Error", msg)));

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
        var mockLogger = new Mock<IMockableLogger>();
        var logger = new TestLoggerAdapter<TestClass>(mockLogger.Object);
        var sut = new TestClass(logger);
        var sideEffectTriggered = false;

        mockLogger.Setup(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()))
            .Callback(() => sideEffectTriggered = true);

        // Act
        sut.DoLogErrorWithException();

        // Assert
        sideEffectTriggered.Should().BeTrue();
    }
}