using Contracts.Products;
using Contracts.Stores;

namespace Contracts;

public record StockImportRequest(int ProductId, int StoreId, int Quantity);
public record StockExportRequest(int ProductId, int StoreId, int Quantity);
public record StockRelocateRequest(int ProductId, int FromStoreId, int ToStoreId, int Quantity);

public record StockResponse(int Quantity, ProductResponse Product, StoreResponse Store);

