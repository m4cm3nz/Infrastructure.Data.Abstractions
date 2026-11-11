# Infrastructure.Data.Abstractions

[![CI](https://github.com/m4cm3nz/Infrastructure.Data.Abstractions/actions/workflows/ci.yml/badge.svg)](https://github.com/m4cm3nz/Infrastructure.Data.Abstractions/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Infrastructure.Data.Abstractions)](https://www.nuget.org/packages/Infrastructure.Data.Abstractions)

A .NET 10.0 collection of data access interfaces. Provides contracts for repositories, unit of work, blob storage, cache, and mapping — keeping your domain and application layers free from infrastructure dependencies.

---

## Installation

```bash
dotnet add package Infrastructure.Data.Abstractions
```

---

## Interfaces

### IRepository\<TEntity, TKey\>

Full CRUD contract for an entity with a typed key.

```csharp
public class ProductRepository : IRepository<Product, Guid>
{
    public Task<IEnumerable<Product>> GetAll() { ... }
    public Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> predicate) { ... }
    public Task<Product> GetByID(Guid id) { ... }
    public Task<bool> FindByID(Guid id) { ... }
    public Task<Guid> Add(Product entity) { ... }
    public Task Update(Product item, Guid id) { ... }
    public Task DeleteBy(Product entity) { ... }
    public Task DeleteBy(Guid id) { ... }
}
```

Register and inject:

```csharp
// registration
services.AddScoped<IRepository<Product, Guid>, ProductRepository>();

// injection
public class ProductService(IRepository<Product, Guid> repository) { }
```

---

### IUnitOfWork

Wraps a database transaction. Obtain the open connection inside the transaction scope.

```csharp
public class OrderService(IUnitOfWork uow, IRepository<Order, int> orders)
{
    public async Task PlaceOrder(Order order)
    {
        uow.Begin();
        try
        {
            await orders.Add(order);
            uow.Commit();
        }
        catch
        {
            uow.Rollback();
            throw;
        }
    }
}
```

---

### IBlobStorage

Upload, download, and check files in a binary store.

```csharp
public class ReportService(IBlobStorage storage)
{
    public async Task SaveReport(Report report, string fileName, CancellationToken cancellationToken)
    {
        await storage.Upload(report, fileName, cancellationToken);
    }

    public async Task<Stream?> LoadReport(string fileName, CancellationToken cancellationToken)
    {
        if (!await storage.ExistsAsync(fileName, cancellationToken))
            return null;

        return await storage.Download(fileName, cancellationToken);
    }
}
```

| Member | Since | Notes |
|---|---|---|
| `Upload<T>(content, fileName)` | 6.0.0 | |
| `Download(fileName)` | 6.0.0 | |
| `Exists(fileName)` | 6.0.0 | Synchronous. Prefer `ExistsAsync`. |
| `Upload<T>(content, fileName, cancellationToken)` | 10.1.0 | Default implementation calls `Upload<T>(content, fileName)`. |
| `Download(fileName, cancellationToken)` | 10.1.0 | Default implementation calls `Download(fileName)`. |
| `ExistsAsync(fileName, cancellationToken = default)` | 10.1.0 | Default implementation returns `Exists(fileName)`. |

---

### IDownloadContent

Download a file as `BinaryData`.

| Member | Since | Notes |
|---|---|---|
| `DownloadContent(fileName)` | 6.0.0 | |
| `DownloadContent(fileName, cancellationToken)` | 10.1.0 | Default implementation calls `DownloadContent(fileName)`. |

The 10.1.0 members are default interface methods, so existing implementations compile without changes. Override them to get true asynchronous and cancellable behavior:

```csharp
public class AzureBlobStorage : IBlobStorage, IDownloadContent
{
    // existing members ...

    public async Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToken = default)
        => await container.GetBlobClient(fileName).ExistsAsync(cancellationToken);
}
```

Call the new members through the interface type (`IBlobStorage`, `IDownloadContent`). A default interface method is not visible on the implementing class unless the class declares it.

---

### IBlobStorageRepository\<TEntity\>

Typed access to blob-backed entities with a work queue.

```csharp
public class DocumentService(IBlobStorageRepository<Document> repository)
{
    public async Task<Document> Load(string key)
    {
        return await repository.GetByKey(key);
    }
}
```

---

### ICacheProvider

Key-based cache abstraction.

```csharp
public class CachedProductService(
    IRepository<Product, Guid> repository,
    ICacheProvider cache)
{
    public async Task<Product> GetProduct(Guid id)
    {
        var key = $"product:{id}";
        var cached = await cache.Get<Product>(key);
        if (cached is not null) return cached;

        var product = await repository.GetByID(id);
        await cache.Add(product, key);
        return product;
    }
}
```

---

### ISimpleMapper\<TIn, TOut\>

One-way mapping between two types.

```csharp
public class ProductMapper : ISimpleMapper<ProductDto, Product>
{
    public Product Map(ProductDto dto) => new Product
    {
        Id   = dto.Id,
        Name = dto.Name
    };
}
```

---

### SensitiveDataAttribute

Marks properties that contain sensitive information. Use in logging, serialization filters, or audit pipelines to suppress sensitive fields.

```csharp
public class Customer
{
    public string Name { get; set; }

    [SensitiveData]
    public string TaxId { get; set; }

    [SensitiveData]
    public string CardNumber { get; set; }
}
```

---

## Composing granular contracts

All query and command interfaces are available individually so you can expose only what a consumer needs:

```csharp
// read-only service dependency
public class ReportService(IGetAll<Order> orders) { }

// write-only service dependency
public class ImportService(IAdd<Product, Guid> products) { }
```

Available granular interfaces:

| Interface | Method |
|---|---|
| `IGetAll<TEntity>` | `GetAll()` |
| `IGetAllByExpression<TEntity>` | `GetAll(predicate)` |
| `IGetById<TEntity, TKey>` | `GetByID(TKey)` |
| `IFindByID<TKey>` | `FindByID(TKey)` |
| `IAdd<TEntity, TKey>` | `Add(TEntity) → TKey` |
| `IUpdate<TEntity, TKey>` | `Update(TEntity, TKey)` |
| `IDelete<TEntity>` | `DeleteBy(TEntity)` |
| `IDeleteById<TKey>` | `DeleteBy(TKey)` |

---

## Migration from v8.1

The `dynamic`-based interfaces (`IRepository<TEntity>`, `IQuery<TEntity>`, `ICommand<TEntity>`, `IGetById<TEntity>`, `IFindByID`, `IAdd<TEntity>`, `IUpdate<TEntity>`, `IDeleteById`) are still present but marked `[Obsolete]`. Existing code compiles with a warning. Migrate by adding the key type:

```csharp
// before
public class OrderRepository : IRepository<Order> { ... }

// after
public class OrderRepository : IRepository<Order, int> { ... }
```

The obsolete interfaces will be removed in a future major release.

---

## License

MIT — see [LICENSE](LICENSE).
