# Testing Rules

## 1. Core Philosophy

-   **Real Tests Only**: Hit actual systems (DB, File, API). No mocks, no fakes.
-   **Zero Fabrication**: If it fails in the real world, the test should fail.

## 2. Test Organization

-   **Co-located Tests**: Every source file MUST have a `#region Tests` at the end.
-   **One Test Class**: One test class per source file (e.g., `UserService` -> `UserServiceTests`).
-   **Inheritance**: All test classes MUST inherit from `TestBase`.
-   **No Sealed**: Test classes must NOT be sealed.

## 3. Test Naming

-   **Format**: `Test[Method][Scenario][Outcome]` (PascalCase).
-   **Prefix**: MUST start with `Test`.
-   **No Underscores**: `TestSaveUserValidInputSucceeds` (✅), `Test_Save_User` (❌).

## 4. Factory Pattern

-   **Location**: `#region Testing` INSIDE the class being tested.
-   **Method**: `public static T Create(...)`.
-   **Mandate**: Must be callable with **NO arguments** (use optional params).
-   **Usage**: Tests use `MyClass.Testing.Create()` to get valid instances.

## 5. TestBase Pattern

-   **Purpose**: Centralized setup/teardown for real resources.
-   **Features**: Temp directories, database initialization, cleanup.
-   **Cleanup**: Automatic via `Dispose()`.
