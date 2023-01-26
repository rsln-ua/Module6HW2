using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ILogger<BaseDataService<ApplicationDbContext>> _logger;
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _logger = logger;
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Update(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Delete(id));
    }

    public Task<CatalogItem?> GetById(int id)
    {
        return _catalogItemRepository.GetById(id);
    }

    public Task<List<CatalogItem>> GetByBrand(string brand)
    {
        return _catalogItemRepository.GetByBrand(brand);
    }

    public Task<List<CatalogItem>> GetByType(string type)
    {
        return _catalogItemRepository.GetByType(type);
    }
}