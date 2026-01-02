using System;
using System.Collections.Generic;
using System.Linq;
using Common.Testing;

namespace Examples;

/// <summary>
/// Smart Enum pattern example.
/// Replaces traditional enums with a class-based approach for type safety and behavior.
/// </summary>
public record ExampleEnum
{
    /// <summary>
    /// Gets the human-readable display name.
    /// Used for UI presentation and logging.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the underlying numeric code.
    /// Used for database persistence and external system integration.
    /// </summary>
    public int Code { get; }

    private ExampleEnum(string value, int code)
    {
        Value = value;
        Code = code;
    }

    /// <summary>
    /// The first example option.
    /// </summary>
    public static readonly ExampleEnum First = new("First", 1);

    /// <summary>
    /// The second example option.
    /// </summary>
    public static readonly ExampleEnum Second = new("Second", 2);

    /// <summary>
    /// The third example option.
    /// </summary>
    public static readonly ExampleEnum Third = new("Third", 3);

    /// <summary>
    /// Gets all available enum options.
    /// Useful for iteration, validation, and populating UI selection lists.
    /// </summary>
    public static IReadOnlyList<ExampleEnum> All { get; } = [First, Second, Third];

    /// <summary>
    /// Determines if the code is even.
    /// Demonstrates how smart enums can encapsulate business logic directly on the type.
    /// </summary>
    /// <returns>True if the code is even; false otherwise</returns>
    public bool IsEven() => Code % 2 == 0;

    #region Testing

    /// <summary>
    /// Factory methods for testing.
    /// </summary>
    public static class Testing
    {
        /// <summary>
        /// Creates a new instance of ExampleEnum for testing purposes.
        /// MANDATORY: Create() method with no required parameters.
        /// </summary>
        /// <param name="value">Optional display value</param>
        /// <param name="code">Optional numeric code</param>
        /// <returns>A new test instance</returns>
        public static ExampleEnum Create(string? value = null, int? code = null)
        {
            return new ExampleEnum(value ?? "Test", code ?? 999);
        }
    }

    #endregion
}

#region Tests

/// <summary>
/// Tests for the ExampleEnum smart enum.
/// Ensures that business logic and factory methods behave as expected.
/// </summary>
public class ExampleEnumTests : TestBase
{
    /// <summary>
    /// Verifies that the IsEven method correctly identifies even and odd numeric codes.
    /// </summary>
    [Fact]
    public void TestIsEvenReturnsCorrectResult()
    {
        Assert.False(ExampleEnum.First.IsEven());
        Assert.True(ExampleEnum.Second.IsEven());
    }

    /// <summary>
    /// Verifies that the testing factory can create instances with custom values.
    /// </summary>
    [Fact]
    public void TestCreateFactoryReturnsCustomInstance()
    {
        var custom = ExampleEnum.Testing.Create(code: 100);
        Assert.True(custom.IsEven());
    }
}

#endregion
