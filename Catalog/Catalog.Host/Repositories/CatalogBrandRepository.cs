using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(ApplicationDbContext dbContext, ILogger<CatalogBrandRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<CatalogBrand>> GetAll()
    {
        var result = await _dbContext.CatalogBrands.ToListAsync();
        return result;
    }

    public async Task<int?> Add(string name)
    {
        var item = await _dbContext.CatalogBrands.AddAsync(new CatalogBrand() { Brand = name });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> Update(int id, string name)
    {
        var item = await _dbContext.CatalogBrands.FirstOrDefaultAsync(el => el.Id == id);

        if (item == null)
        {
            return default;
        }

        _dbContext.Entry(item).CurrentValues.SetValues(new CatalogBrand() { Id = id, Brand = name });
        await _dbContext.SaveChangesAsync();

        return item.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.CatalogBrands.FirstOrDefaultAsync(el => el.Id == id);

        if (item == null)
        {
            return default;
        }

        _dbContext.Entry(item).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();

        return item.Id;
    }
}