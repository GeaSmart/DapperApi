using DapperApi.Entities;
using DapperApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var product = await _repository.GetByIdAsync(Id);
        if (product is null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        var newId = await _repository.InsertAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    [HttpGet("{id}/with-tags")]
    public async Task<IActionResult> GetProductWithTags(Guid id)
    {
        var product = await _repository.GetProductWithTagsAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
}