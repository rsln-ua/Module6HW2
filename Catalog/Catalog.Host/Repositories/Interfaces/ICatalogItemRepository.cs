using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Delete(int id);
    Task<CatalogItem?> GetById(int id);
    Task<List<CatalogItem>> GetByBrand(string brand);
    Task<List<CatalogItem>> GetByType(string type);
}