using System;
using System.Threading.Tasks;
using Common.Testing;

namespace Examples;

/// <summary>
/// Example of a stateful service that manages a resource.
/// Demonstrates dependency injection, async methods, and proper factory patterns.
/// </summary>
public class ExampleService : IExampleService
{
    private readonly ILogger _logger;
    private int _operationCount;

    /// <summary>
    /// Initializes a new instance of the ExampleService.
    /// Private to enforce use of the From() factory method.
    /// </summary>
    private ExampleService(ILogger logger)
    {
        _logger = logger;
        _operationCount = 0;
    }

    /// <summary>
    /// Creates a new instance of ExampleService from its dependencies.
    /// </summary>
    /// <param name="logger">The logger dependency</param>
    /// <returns>A configured ExampleService</returns>
    public static ExampleService From(ILogger logger)
    {
        // No null check needed on non-nullable logger
        return new ExampleService(logger);
    }

    /// <summary>
    /// Performs a complex operation and tracks usage.
    /// </summary>
    public async Task<string> ProcessOperationAsync(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be empty", nameof(input));

        _operationCount++;
        _logger.Log($"Processing operation #{_operationCount}: {input}");

        await Task.Delay(10);

        return $"Processed: {input} (Op #{_operationCount})";
    }

    /// <summary>
    /// Gets the current operation count.
    /// </summary>
    public int GetOperationCount() => _operationCount;

    #region Testing

    /// <summary>
    /// Factory for creating test instances.
    /// </summary>
    public static class Testing
    {
        /// <summary>
        /// Creates a new instance of ExampleService for testing.
        /// MANDATORY: Create() method with no required parameters.
        /// </summary>
        /// <param name="logger">Optional logger override</param>
        /// <returns>A new test instance</returns>
        public static ExampleService Create(ILogger? logger = null)
        {
            return ExampleService.From(logger ?? new TestLogger());
        }
    }

    /// <summary>
    /// Simple test logger record.
    /// Records are allowed to have auto-generated public constructors.
    /// </summary>
    public record TestLogger : ILogger
    {
        /// <summary>
        /// Logs a message to nowhere for testing.
        /// </summary>
        public void Log(string message) { }
    }

    #endregion
}

/// <summary>
/// Logger interface for dependency injection example.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Logs a message.
    /// </summary>
    void Log(string message);
}

#region Tests

/// <summary>
/// Tests for ExampleService.
/// </summary>
public class ExampleServiceTests : TestBase
{
    /// <summary>
    /// Verifies that processing an operation increments the internal counter.
    /// </summary>
    [Fact]
    public async Task TestProcessOperationAsyncValidInputIncrementsCount()
    {
        var service = ExampleService.Testing.Create();
        var result1 = await service.ProcessOperationAsync("First");
        var result2 = await service.ProcessOperationAsync("Second");
        Assert.Equal("Processed: First (Op #1)", result1);
        Assert.Equal("Processed: Second (Op #2)", result2);
        Assert.Equal(2, service.GetOperationCount());
    }

    /// <summary>
    /// Verifies that empty input throws an ArgumentException.
    /// </summary>
    [Fact]
    public async Task TestProcessOperationAsyncEmptyInputThrowsException()
    {
        var service = ExampleService.Testing.Create();
        await Assert.ThrowsAsync<ArgumentException>(() => service.ProcessOperationAsync(""));
    }
}

#endregion
