using System.Threading.Tasks;

namespace Examples;

/// <summary>
/// Interface for the ExampleService.
/// Interfaces generally do not have tests unless they have default implementations.
/// </summary>
public interface IExampleService
{
    /// <summary>
    /// Processes an operation.
    /// </summary>
    Task<string> ProcessOperationAsync(string input);

    /// <summary>
    /// Gets the current count.
    /// </summary>
    int GetOperationCount();
}
