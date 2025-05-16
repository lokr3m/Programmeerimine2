using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/Product")]
[ApiController]
public class ProductApiController : ControllerBase
{
    private readonly IProductsService _service;

    public ProductApiController(IProductsService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        var result = await _service.List(1, 10000, null);
        return result.Results;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _service.Get(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        await _service.Save(product);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest();
        await _service.Save(product);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}