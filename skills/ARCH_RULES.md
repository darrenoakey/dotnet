# Architecture Rules

## 1. Project Structure

-   **`src/`**: All source code. Contains solution file.
-   **`output/`**: Build artifacts (bin/obj). Configured in `Directory.Build.props`.
-   **`local/`**: Local-only files (ignored by git).

## 2. "HOW" vs "WHAT"

-   **Common Project ("HOW")**: ~95% of code.
    -   Generic, domain-agnostic utilities (Extensions, Helpers).
    -   Organized by subject (Text, Net, Data).
    -   Static, stateless functions preferred.
    -   *See `StringExtensions.cs`.*
-   **Feature Projects ("WHAT")**: ~5% of code.
    -   Business logic, domain models, UI.
    -   Organized by Feature (Vertical Slices).

## 3. Vertical Slices (Anti-Layering)

-   **Group by Feature**: `Credentials/`, `Projects/`, `Users/`.
-   **Co-location**: UI, API, Logic, and Data models for a feature live together.
-   **Forbidden Directories**: `Controllers/`, `Services/`, `Models/`, `Pages/` (Horizontal Layering is BANNED).

## 4. File Organization

-   **One Type Per File**: Each file contains exactly one public type.
-   **File Name**: Must match the type name exactly.
-   **Exceptions**: Private helper types inside the main type.

## 5. Dependency Rules

-   **Common**: Depends on NOTHING (except standard libs).
-   **Features**: Depend on Common.
-   **UI/API**: Depend on Features (or contain them).
