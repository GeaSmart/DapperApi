using DapperApi.Dtos;
using DapperApi.Entities;

namespace DapperApi.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Guid> InsertAsync(Product product);
    Task<ProductWithTagsDto?> GetProductWithTagsAsync(Guid id);
}