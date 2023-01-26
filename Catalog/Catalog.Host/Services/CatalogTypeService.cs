using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly ILogger<BaseDataService<ApplicationDbContext>> _logger;
    private readonly ICatalogTypeRepository _catalogTypeRepository;

    public CatalogTypeService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, ICatalogTypeRepository catalogTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _logger = logger;
        _catalogTypeRepository = catalogTypeRepository;
    }

    public Task<List<CatalogType>> GetAll()
    {
        return _catalogTypeRepository.GetAll();
    }

    public Task<int?> Add(string name)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Add(name));
    }

    public Task<int?> Update(int id, string name)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Update(id, name));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Delete(id));
    }
}