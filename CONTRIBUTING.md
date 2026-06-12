# Contributing

Thank you for your interest in contributing to **Infrastructure.Data.Abstractions**.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Any editor — Visual Studio, Rider, or VS Code with the C# extension

## Setting up

```bash
git clone https://github.com/m4cm3nz/Infrastructure.Data.Abstractions.git
cd Infrastructure.Data.Abstractions
dotnet build
```

## How to contribute

### Reporting a bug

Open an issue using the **Bug report** template. Include a minimal reproduction snippet.

### Proposing a new interface

Open an issue using the **Feature request** template **before writing any code**. Describe the contract you need, why it cannot be expressed with the existing interfaces, and a sketch of the proposed API.

### Submitting a pull request

1. Fork the repository and create a branch from `master`.
2. Make your changes following the conventions below.
3. Run `dotnet build` — must pass with zero warnings.
4. Open a PR against `master` using the provided template.

---

## Conventions

### Interface design

Every interface in this library follows these rules:

- Declare only the minimal contract — no implementation details, no infrastructure leakage.
- Use generic type parameters instead of `dynamic` for identity types (`TKey`) and mapped types.
- Keep methods `async Task`-based. Synchronous I/O members are only acceptable when they carry no I/O cost (e.g., an in-memory check).
- Separate read (query) and write (command) concerns. Do not create a single "service" interface that mixes both — compose them instead.
- New interfaces that extend existing ones should add a `TKey` or other type parameter rather than duplicate the existing contract with `object` or `dynamic`.

### Deprecation policy

When replacing an existing interface, keep the old one with `[Obsolete("Use XYZ instead.")]`. It will be removed in the next major version. Never remove a public interface without a deprecation cycle.

### Commit messages

Use the conventional commits format:

```
feat: add IRepository<TEntity, TKey> with typed key
fix: mark IGetById<TEntity> as obsolete with correct message
docs: update README migration guide
chore: bump version to 8.2.0
```

### Comments

Write no comments unless the *why* is non-obvious — a hidden constraint, a design decision that contradicts intuition, or a workaround for a specific framework limitation. Do not describe what the code does.

---

## Branch and release model

- `master` is the stable branch. All PRs target `master`.
- Versions follow [Semantic Versioning](https://semver.org). Breaking changes increment the major version. The major version tracks the target .NET version (e.g., `8.x.y` targets .NET 8).
- A GitHub Release and a NuGet package are published automatically when a `v*` tag is pushed to `master`.
