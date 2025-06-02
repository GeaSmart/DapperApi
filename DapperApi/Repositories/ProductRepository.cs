using DapperApi.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DapperApi.Dtos;

namespace DapperApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Products";
        using var db = Connection;
        return await db.QueryAsync<Product>(sql);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Products WHERE Id = @Id";
        using var db = Connection;
        return await db.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }
    public async Task<Guid> InsertAsync(Product product)
    {
        product.Id = Guid.NewGuid();

        const string sql = @"
        INSERT INTO Products (Id, Description, Price)
        VALUES (@Id, @Description, @Price);";

        using var db = Connection;
        await db.ExecuteAsync(sql, new
        {
            product.Id,
            product.Description,
            product.Price
        });

        return product.Id;
    }

    public async Task<ProductWithTagsDto?> GetProductWithTagsAsync(Guid id)
    {
        const string sql = @"
        SELECT 
            p.Id, p.Description, p.Price,
            t.Id AS TagId, t.Name
        FROM Products p
        LEFT JOIN Tags t ON p.Id = t.ProductId
        WHERE p.Id = @Id;";

        using var db = Connection;

        ProductWithTagsDto? product = null;

        var lookup = new Dictionary<Guid, ProductWithTagsDto>();

        await db.QueryAsync<ProductWithTagsDto, Tags, ProductWithTagsDto>(
            sql,
            (p, tag) =>
            {
                if (!lookup.TryGetValue(p.Id, out product))
                {
                    product = p;
                    product.Tags = new List<Tags>();
                    lookup.Add(product.Id, product);
                }

                if (tag != null && tag.TagId != 0)
                    product.Tags.Add(tag);

                return product;
            },
            new { Id = id },
            splitOn: "TagId"
        );

        return lookup.Values.FirstOrDefault();
    }
}