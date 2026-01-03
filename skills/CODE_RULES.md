# Coding Rules & Standards

## 1. Naming & Style

-   **PascalCase**: Classes, Methods, Properties, Constants, Interfaces (`I` prefix).
-   **camelCase**: Fields (`_` prefix), Parameters, Local variables.
-   **Test Methods**: MUST start with `Test` + PascalCase. NO underscores. (e.g., `TestUserLoginSuccess`).
-   **Namespaces**: File-scoped namespaces matching directory structure.
-   **Braces**: ALWAYS use braces, even for single-line statements.
    -   *Exception*: Guard clauses pattern (see below).
-   **Collection Expressions**: Use `[]` syntax (e.g., `List<string> items = [];`).

## 2. Immutability

-   **Records**: Use `record` for all data types. **NEVER** use `class` for data (POCOs).
-   **No Setters**: Properties must be init-only or get-only. No public setters.
-   **Collections**: Use `IReadOnlyList<T>` or `ImmutableArray<T>`. Never expose mutable lists.

## 3. Smart Enums

-   **No `enum`**: Traditional `enum` is FORBIDDEN.
-   **Enum Classes**: Use the `sealed record` pattern with private constructor and static instances.
-   **Behavior**: Encapsulate logic within the enum class.
-   *See `ExampleEnum.cs`.*

## 4. Guard Clauses

-   **No Null Checks on Non-Nullable**: Trust the compiler. `Nullable` is enabled.
-   **Validation**: Validate inputs at the start of methods.
-   **Pattern**: Single-line `if` without braces is allowed ONLY for simple throws/returns.
    ```csharp
    if (input == null) throw new ArgumentNullException(nameof(input));
    ```

## 5. Documentation

-   **Mandatory**: XML docs (`///`) on ALL public members (classes, methods, props).
-   **Content**: Explain **WHY**, not WHAT.
-   **Params**: `<param>` tags are MANDATORY. Do not remove them.

## 6. Exception Handling Rules

**Catching exceptions is FORBIDDEN except in these 4 scenarios:**

1.  **Entry Points (Generic Allowed)**: At the top level (UI event, API controller, Main), you **MUST** catch generic `Exception`. Log it, record it, and reset system state to "safe" (as if the operation never happened).
2.  **Batch Processing (Generic Allowed)**: When processing list items, catch generic `Exception` for individual failures. Log the error, partition the failed item, and **continue** processing the rest.
3.  **Context Enrichment (Generic Allowed)**: Catch generic `Exception` to wrap/enrich it with context (e.g., Trade ID) and **rethrow** it immediately.
4.  **Retry Logic (Specific Allowed)**: In low-level I/O, catch exceptions for exponential backoff/retry (e.g., external API rate limits). This is the only time catching specific exceptions is preferred, though generic is also allowed if necessary.

## 7. Prohibited Patterns (Zero Tolerance)

❌ **Null Checks on Non-Nullable Types**
❌ **Horizontal Layering** (Controllers/Services/Models directories)
❌ **Mutable POCOs**
❌ **Underscores in Method Names**
❌ **`TODO` Comments**
❌ **Public Constructors** (except for records/DTOs)
