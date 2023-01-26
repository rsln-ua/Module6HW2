using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<List<CatalogBrand>> GetAll();
    Task<int?> Add(string name);
    Task<int?> Update(int id, string name);
    Task<int?> Delete(int id);
}