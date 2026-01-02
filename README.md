![](banner.jpg)

# .NET Development Skill

A Claude Code skill that enforces strict .NET development standards with zero-fabrication testing, immutability-first design, and rigorous quality gates.

## Purpose

This skill provides mandatory coding standards and architectural rules for .NET/C# projects. It ensures:

- **Zero-fabrication testing**: All tests hit real systems—no mocks, no fakes
- **Co-located tests**: Tests live in the same file as the code they test
- **Immutability by default**: Record types only, no public setters
- **Clean architecture**: Clear separation between reusable utilities and business logic
- **Zero tolerance for warnings**: All warnings are treated as errors

## How to Use

Invoke the skill when working on .NET projects:

```
/dotnet
```

The skill will guide Claude to follow strict .NET development standards including:

- One type per file with matching file names
- Tests in `#region Tests` at the end of each file
- Smart enums instead of traditional enums
- Private constructors with factory methods
- XML documentation on all public members

## Examples

### Creating a New Service

When you ask Claude to create a service:

```
Create a UserService that handles user registration
```

Claude will generate code following the skill's patterns:
- Private constructor with `From()` factory method
- Async methods where appropriate
- Co-located tests in `#region Tests`
- `Testing.Create()` factory for test instances

### Adding a Smart Enum

When you need an enumeration:

```
Create a PaymentStatus enum with Pending, Completed, and Failed states
```

Claude will create a smart enum using the sealed record pattern with:
- Static readonly instances
- Encapsulated behavior methods
- An `All` property for iteration
- Full test coverage

### Writing Extension Methods

When you need utility methods:

```
Add a string extension method to convert to slug format
```

Claude will add to the appropriate extensions file:
- Static extension method
- XML documentation explaining why the method exists
- Co-located tests covering edge cases

## Installation

1. Create the skill directory:
   ```bash
   mkdir -p ~/.claude/skills/dotnet
   ```

2. Copy all skill files to the directory:
   ```bash
   cp SKILL.md CODE_RULES.md ARCH_RULES.md TEST_RULES.md SETUP.md EXAMPLES.md ~/.claude/skills/dotnet/
   cp *.cs ~/.claude/skills/dotnet/
   ```

3. Register the skill in your Claude Code configuration by adding to `~/.claude/settings.json`:
   ```json
   {
     "skills": {
       "dotnet": {
         "path": "~/.claude/skills/dotnet",
         "description": "Strict .NET development standards with zero-fabrication testing"
       }
     }
   }
   ```

## Key Standards Enforced

| Rule | Description |
|------|-------------|
| One Type Per File | Each file contains exactly one public type |
| Co-located Tests | Tests in `#region Tests` at end of source file |
| Record Types Only | No mutable POCOs—use `record` for all data |
| No Mocks | Tests must hit real systems |
| Factory Pattern | Use `Testing.Create()` for test instances |
| Smart Enums | Use sealed record pattern, not `enum` |
| Zero Warnings | All warnings treated as errors |

## Project Structure

The skill enforces this directory structure:

```
MySolution/
├── src/           # All source code
│   ├── Common/    # Reusable utilities (~95% of code)
│   └── Features/  # Business logic (~5% of code)
├── output/        # Build artifacts (gitignored)
└── local/         # Local-only files (gitignored)
```

## Quick Reference

### Test Method Naming
```
Test[Method][Scenario][Outcome]
```
Example: `TestProcessOrderValidInputReturnsConfirmation`

### Factory Pattern
```csharp
public static class Testing
{
    public static MyClass Create(string? optionalParam = null)
    {
        return new MyClass(optionalParam ?? "default");
    }
}
```

### File Structure
```csharp
namespace MyApp.Features;

/// <summary>
/// XML docs required on all public members.
/// </summary>
public class MyService
{
    // Implementation...

    #region Testing
    public static class Testing { /* ... */ }
    #endregion
}

#region Tests
public class MyServiceTests : TestBase { /* ... */ }
#endregion
```