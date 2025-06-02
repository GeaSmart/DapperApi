using DapperApi.Entities;

namespace DapperApi.Dtos;

public class ProductWithTagsDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<Tags> Tags { get; set; } = new();
}