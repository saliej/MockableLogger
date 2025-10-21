# MockableLogger

A logger adapter for `Microsoft.Extensions.Logging.ILogger<T>` that enables easy mocking and verification of log calls in unit tests.

## The Problem

Microsoft's `ILogger<T>` interface uses extension methods for common logging operations (`LogInformation`, `LogError`, etc.). These extension methods cannot be directly mocked because they're not part of the interface contract. This makes it difficult to verify that specific log messages were written during unit tests.

For example, consider the following class:
```csharp
    public class TestClass
    {
        private readonly ILogger<TestClass> _logger;
        public TestClass(ILogger<TestClass> logger)
        {
            _logger = logger;
        }

        public void DoLogInfoWithArgs(string name)
        {
            _logger.LogInformation("Info with {Name}", name);
        }
    }
```


This Unit Test using Moq will fail:
```csharp
    [Fact]
    public void TestLogInformationExtension()
    {
        var mockLogger = new Mock<ILogger<TestClass>>();
        var sut = new TestClass(mockLogger.Object);

        sut.DoLogInfoWithArgs("name");

        mockLogger.Verify(m => m.LogInformation("Info with {Name}", "name"), Times.Once);
    }
```

```
    System.NotSupportedException
    Unsupported expression: m => m.LogInformation("Info with {Name}", new[] { "name" })
    Extension methods (here: LoggerExtensions.LogInformation) may not be used in setup / verification expressions.
       at Moq.Guard.IsOverridable(MethodInfo method, Expression expression) in /_/src/Moq/Guard.cs:line 87
```

The following Unit Test using NSubstitute has the same problem, but with a less-specific error message:
```csharp
    [Fact]
    public void TestLogInformationExtension_NSubstitute()
    {
        var mockLogger = Substitute.For<ILogger<TestClass>>();
        var sut = new TestClass(mockLogger);

        sut.DoLogInfoWithArgs("name");

        mockLogger.Received(1).LogInformation("Info with {Name}", "name");
    }
```

```
    NSubstitute.Exceptions.ReceivedCallsException
    Expected to receive exactly 1 call matching:
	    Log<FormattedLogValues>(Information, 0, Info with name, <null>, Func<FormattedLogValues, Exception, String>)
    Actually received no matching calls.
    Received 1 non-matching call (non-matching arguments indicated with '*' characters):
	    Log<FormattedLogValues>(Information, 0, *Info with name*, <null>, Func<FormattedLogValues, Exception, String>)

```

JustMock Lite fails with:
```
Telerik.JustMock.Core.ElevatedMockingException
Cannot mock 'Microsoft.Extensions.Logging.LoggerExtensions'. 
JustMock Lite can only mock interface members, virtual/abstract members in non-sealed classes, delegates and all members on classes derived from MarshalByRefObject on instances created with Mock.Create or Mock.CreateLike. 
For any other scenario you need to use the full version of JustMock.
```

## The Solution

MockableLogger provides:
1. **`IMockableLogger`** - An interface with explicit method signatures for all logging operations
2. **`TestLoggerAdapter<T>`** - An adapter that implements `ILogger<T>` and routes calls to `IMockableLogger`
3. Full support for structured logging, exceptions, EventIds, and log scopes

### Using NSubstitute

```csharp
    [Fact]
    public void TestLogInformationExtension()
    {
        var mockLogger = Substitute.For<IMockableLogger>();
        var adapter = new TestLoggerAdapter<TestClass>(mockLogger); 
        var sut = new TestClass(adapter);

        sut.DoLogInfoWithArgs("name");

        mockLogger.Received(1).LogInformation("Info with {Name}", "name");
    }
```

### Using Moq

```csharp
    [Fact]
    public void TestLogInformationExtension()
    {
        var mockLogger = new Mock<IMockableLogger>();
        var adapter = new TestLoggerAdapter<TestClass>(mockLogger.Object); 
        var sut = new TestClass(adapter);

        sut.DoLogInfoWithArgs("name");

        mockLogger.Verify(m => m.LogInformation("Info with {Name}", "name"), Times.Once);
    }
}
```

### Using JustMock Lite

```csharp
    [Fact]
    public void TestLogInformationExtension()
    {
        var mockLogger = Mock.Create<IMockableLogger>();
        var adapter = new TestLoggerAdapter<TestClass>(mockLogger); 
        var sut = new TestClass(adapter);

        sut.DoLogInfoWithArgs("name");

        Mock.Assert(() => mockLogger.LogInformation("Info with {Name}", "name"));
    }
```

## Installation

```bash
# Install the package
dotnet add package MockableLogger
```

## Comparison with Microsoft's FakeLogger

Microsoft provides `FakeLogger<T>` in the `Microsoft.Extensions.Logging.Testing` package, but it has different goals and trade-offs:

### Microsoft's FakeLogger

```csharp
using Microsoft.Extensions.Logging.Testing;

[Fact]
public void MyMethod_LogsMessage_WithFakeLogger()
{
    // Arrange
    var fakeLogger = new FakeLogger<MyService>();
    var service = new MyService(fakeLogger);

    // Act
    service.DoSomething();

    // Assert - inspect the collector
    var logRecord = Assert.Single(fakeLogger.Collector.GetSnapshot());
    Assert.Equal(LogLevel.Information, logRecord.Level);
    Assert.Equal("Operation completed successfully", logRecord.Message);
}
```

**FakeLogger Characteristics:**
- Captures all log entries for inspection
- No mocking framework required
- More verbose assertions (inspect collection, check properties)
- Harder to verify specific method overloads were called
- Less expressive for complex verification scenarios

**MockableLogger Characteristics:**
- Direct, expressive verification syntax
- Leverage full power of mocking frameworks (argument matching, verification, etc.)
- Verify specific overloads (with/without EventId, Exception, etc.)
- Requires a mocking framework

### When to Use Each

**Use FakeLogger when:**
- You want to avoid mocking frameworks
- You need to inspect all logged entries
- You're doing simple log verification

**Use MockableLogger when:**
- You already use a compatible mocking framework in your test suite
- You need precise control over verification (specific overloads, argument matching)
- You want concise, readable test assertions
- You need to enable testing callbacks
- You need to setup behaviors (e.g., `IsEnabled()` returns false)
