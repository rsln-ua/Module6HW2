using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogTypeService
{
    Task<List<CatalogType>> GetAll();
    Task<int?> Add(string name);
    Task<int?> Update(int id, string name);
    Task<int?> Delete(int id);
}