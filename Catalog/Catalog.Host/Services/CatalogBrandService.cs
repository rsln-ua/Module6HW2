using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ILogger<BaseDataService<ApplicationDbContext>> _logger;
    private readonly ICatalogBrandRepository _catalogBrandRepository;

    public CatalogBrandService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
    {
        _logger = logger;
        _catalogBrandRepository = catalogBrandRepository;
    }

    public Task<List<CatalogBrand>> GetAll()
    {
        return _catalogBrandRepository.GetAll();
    }

    public Task<int?> Add(string name)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Add(name));
    }

    public Task<int?> Update(int id, string name)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Update(id, name));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Delete(id));
    }
}