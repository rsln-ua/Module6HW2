namespace Catalog.Host.Models.Requests;

public class UpdateCatalogBrandRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}