# Project Setup Guide

## 1. Initialize Solution

```bash
mkdir MySolution && cd MySolution
mkdir src output local
cd src
dotnet new sln -n MySolution
```

## 2. Configure Build Props

Copy the standard `Directory.Build.props` to your `src` folder. This handles:
-   Nullable reference types (Enabled)
-   Warnings as Errors (Enabled)
-   Output redirection to `output/`
-   Centralized package versions

## 3. Create Common Library

```bash
dotnet new classlib -n Common
rm Common/Class1.cs
dotnet sln add Common/Common.csproj
```

## 4. Create Feature Project

```bash
dotnet new console -n MyApp
dotnet sln add MyApp/MyApp.csproj
dotnet add MyApp/MyApp.csproj reference Common/Common.csproj
```

## 5. Standard Gitignore

Ensure your `.gitignore` excludes:
-   `output/`
-   `local/`
-   `.idea/`, `.vscode/`, `*.user`, etc.
