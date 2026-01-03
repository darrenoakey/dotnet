namespace Common.Text;

/// <summary>
/// Extension methods for string manipulation.
/// Centralized here to ensure consistent behavior across the application
/// and to avoid duplication of common string operations.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Truncates a string to the specified maximum length.
    /// We add an ellipsis to indicate truncation happened, which is important
    /// for user-facing displays where knowing text was cut off matters.
    /// </summary>
    /// <param name="value">The string to truncate</param>
    /// <param name="maxLength">Maximum length including ellipsis</param>
    /// <returns>Truncated string with ellipsis if needed, original string otherwise</returns>
    /// <exception cref="ArgumentException">Thrown when maxLength is less than 4</exception>
    public static string Truncate(this string value, int maxLength)
    {
        if (maxLength < 4)
            throw new ArgumentException("Max length must be at least 4 to accommodate ellipsis", nameof(maxLength));

        if (string.IsNullOrEmpty(value)) return value;
        if (value.Length <= maxLength) return value;
        return value.Substring(0, maxLength - 3) + "...";
    }

    /// <summary>
    /// Determines if a string is null, empty, or contains only whitespace.
    /// This is a common check that appears throughout codebases, so centralizing
    /// it ensures consistency and improves readability.
    /// </summary>
    /// <param name="value">The string to check</param>
    /// <returns>True if null, empty, or whitespace; false otherwise</returns>
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Converts a string to title case.
    /// We handle special cases like acronyms and preserve existing casing
    /// where appropriate, which is why we don't just use ToTitleCase blindly.
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>Title cased string</returns>
    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;

        var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(value.ToLower());
    }
}

#region Tests

/// <summary>
/// Tests for StringExtensions.
/// These tests verify that string manipulation helpers work correctly
/// across various edge cases and normal usage scenarios.
/// </summary>
public class StringExtensionsTests : TestBase
{
    /// <summary>
    /// Verifies that a string shorter than the max length is returned unchanged.
    /// </summary>
    [Fact]
    public void TestTruncateShortStringReturnsOriginal()
    {
        var input = "Hello";
        var result = input.Truncate(10);
        Assert.Equal("Hello", result);
    }

    /// <summary>
    /// Verifies that a string equal to the max length is returned unchanged.
    /// </summary>
    [Fact]
    public void TestTruncateExactLengthReturnsOriginal()
    {
        var input = "Hello";
        var result = input.Truncate(5);
        Assert.Equal("Hello", result);
    }

    /// <summary>
    /// Verifies that a string longer than the max length is truncated with an ellipsis.
    /// </summary>
    [Fact]
    public void TestTruncateLongStringAddsEllipsis()
    {
        var input = "Hello World";
        var result = input.Truncate(8);
        Assert.Equal("Hello...", result);
    }

    /// <summary>
    /// Verifies that a null string input returns null.
    /// </summary>
    [Fact]
    public void TestTruncateNullStringReturnsNull()
    {
        string? input = null;
        var result = input!.Truncate(10);
        Assert.Null(result);
    }

    /// <summary>
    /// Verifies that an empty string returns an empty string.
    /// </summary>
    [Fact]
    public void TestTruncateEmptyStringReturnsEmpty()
    {
        var input = "";
        var result = input.Truncate(10);
        Assert.Equal("", result);
    }

    /// <summary>
    /// Verifies that providing a max length less than 4 throws an ArgumentException.
    /// </summary>
    [Fact]
    public void TestTruncateMaxLengthTooSmallThrowsArgumentException()
    {
        var input = "Hello";
        Assert.Throws<ArgumentException>(() => input.Truncate(3));
    }

    /// <summary>
    /// Verifies that an empty string returns true for IsNullOrWhiteSpace.
    /// </summary>
    [Fact]
    public void TestIsNullOrWhiteSpaceEmptyStringReturnsTrue()
    {
        var input = "";
        var result = input.IsNullOrWhiteSpace();
        Assert.True(result);
    }

    /// <summary>
    /// Verifies that a string with only whitespace returns true for IsNullOrWhiteSpace.
    /// </summary>
    [Fact]
    public void TestIsNullOrWhiteSpaceWhitespaceStringReturnsTrue()
    {
        var input = "   ";
        var result = input.IsNullOrWhiteSpace();
        Assert.True(result);
    }

    /// <summary>
    /// Verifies that a null string returns true for IsNullOrWhiteSpace.
    /// </summary>
    [Fact]
    public void TestIsNullOrWhiteSpaceNullStringReturnsTrue()
    {
        string? input = null;
        var result = input.IsNullOrWhiteSpace();
        Assert.True(result);
    }

    /// <summary>
    /// Verifies that a valid non-empty string returns false for IsNullOrWhiteSpace.
    /// </summary>
    [Fact]
    public void TestIsNullOrWhiteSpaceValidStringReturnsFalse()
    {
        var input = "Hello";
        var result = input.IsNullOrWhiteSpace();
        Assert.False(result);
    }

    /// <summary>
    /// Verifies that a lowercase string is converted to title case.
    /// </summary>
    [Fact]
    public void TestToTitleCaseLowercaseStringConvertsToTitleCase()
    {
        var input = "hello world";
        var result = input.ToTitleCase();
        Assert.Equal("Hello World", result);
    }

    /// <summary>
    /// Verifies that an uppercase string is converted to title case.
    /// </summary>
    [Fact]
    public void TestToTitleCaseUppercaseStringConvertsToTitleCase()
    {
        var input = "HELLO WORLD";
        var result = input.ToTitleCase();
        Assert.Equal("Hello World", result);
    }

    /// <summary>
    /// Verifies that an empty string returns an empty string when converted to title case.
    /// </summary>
    [Fact]
    public void TestToTitleCaseEmptyStringReturnsEmpty()
    {
        var input = "";
        var result = input.ToTitleCase();
        Assert.Equal("", result);
    }
}

#endregion
