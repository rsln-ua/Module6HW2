using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Delete(int id);
    Task<CatalogItem?> GetById(int id);
    Task<List<CatalogItem>> GetByBrand(string brand);
    Task<List<CatalogItem>> GetByType(string type);
}