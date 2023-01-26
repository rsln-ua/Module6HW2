namespace Catalog.Host.Models.Requests;

public class UpdateCatalogTypeRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}