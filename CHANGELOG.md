# Changelog

## [Unreleased]

---

## [10.0.0] — 2026-06-12

### Breaking changes

- Upgraded target framework from .NET 8.0 to .NET 10.0.
- `System.Memory.Data` package dependency removed — `BinaryData` is part of the .NET 10 BCL.

### New features

- `IRepository<TEntity, TKey>`: typed-key variant replacing the `dynamic`-based `IRepository<TEntity>`.
- `IQuery<TEntity, TKey>`: typed-key composite query interface (`IGetById<TEntity, TKey>` + `IFindByID<TKey>` + `IGetAll` + `IGetAllByExpression`).
- `ICommand<TEntity, TKey>`: typed-key composite command interface (`IAdd<TEntity, TKey>` + `IUpdate<TEntity, TKey>` + `IDelete<TEntity>` + `IDeleteById<TKey>`).
- Granular typed-key interfaces: `IGetById<TEntity, TKey>`, `IFindByID<TKey>`, `IAdd<TEntity, TKey>`, `IUpdate<TEntity, TKey>`, `IDeleteById<TKey>`.
- `IAdd<TEntity, TKey>` returns `Task<TKey>` instead of `Task<dynamic>`, making the identity of the newly created entity type-safe.

### Deprecations

The following interfaces are now marked `[Obsolete]` and will be removed in v11. Replace with the `TKey` variants listed in the migration guide in the README.

- `IRepository<TEntity>`
- `IQuery<TEntity>`
- `ICommand<TEntity>`
- `IGetById<TEntity>` (dynamic key)
- `IFindByID` (dynamic key)
- `IAdd<TEntity>` (dynamic return)
- `IUpdate<TEntity>` (dynamic key)
- `IDeleteById` (dynamic key)

---

## [8.1.0] — 2024-11-01

### Improvements

- README included in the NuGet package (`PackageReadmeFile`).
- Package description and tags updated.

---

## [8.0.0] — 2024-03-01

### Breaking changes

- Upgraded target framework from .NET 6.0 to .NET 8.0.

---

## [6.0.0]

- Initial release targeting .NET 6.0.
- Interfaces: `IRepository<TEntity>`, `IQuery<TEntity>`, `ICommand<TEntity>`, `IUnitOfWork`, `IBlobStorage`, `IBlobStorageRepository<TEntity>`, `ICacheProvider`, `ISimpleMapper<TIn, TOut>`, `SensitiveDataAttribute`.
