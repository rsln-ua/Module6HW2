using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(el => el.Id == id);

        if (item == null)
        {
            return default;
        }

        _dbContext.Entry(item).CurrentValues.SetValues(new CatalogItem()
        {
            Id = id, Name = name, Description = description, Price = price, AvailableStock = availableStock, CatalogBrandId = catalogBrandId, CatalogTypeId = catalogBrandId,
            PictureFileName = pictureFileName
        });
        await _dbContext.SaveChangesAsync();

        return item.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(el => el.Id == id);

        if (item == null)
        {
            return default;
        }

        _dbContext.Entry(item).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<CatalogItem?> GetById(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(el => el.Id == id);
        return item;
    }

    public async Task<List<CatalogItem>> GetByBrand(string brand)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(el => el.CatalogBrand.Brand == brand).ToListAsync();
        return items;
    }

    public async Task<List<CatalogItem>> GetByType(string type)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(el => el.CatalogType.Type == type).ToListAsync();
        return items;
    }
}