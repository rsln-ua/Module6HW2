using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(ApplicationDbContext dbContext, ILogger<CatalogTypeRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<CatalogType>> GetAll()
    {
        var result = await _dbContext.CatalogTypes.ToListAsync();
        return result;
    }

    public async Task<int?> Add(string name)
    {
        var item = await _dbContext.CatalogTypes.AddAsync(new CatalogType() { Type = name });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> Update(int id, string name)
    {
        var item = await _dbContext.CatalogTypes.FirstOrDefaultAsync(el => el.Id == id);

        if (item == null)
        {
            return default;
        }

        _dbContext.Entry(item).CurrentValues.SetValues(new CatalogType() { Id = id, Type = name });
        await _dbContext.SaveChangesAsync();

        return item.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.CatalogTypes.FirstOrDefaultAsync(el => el.Id == id);

        if (item == null)
        {
            return default;
        }

        _dbContext.Entry(item).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();

        return item.Id;
    }
}