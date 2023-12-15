using System.Net;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Product.Repositories.Interfaces;

namespace Product.Controller;

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductController(IProductRepository repository, ILogger<ProductController> logger, IPublishEndpoint publishEndpoint)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    [HttpGet("products", Name = "GetProducts")]
    [ProducesResponseType(typeof(IEnumerable<Entities.Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Product>>> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }
    
    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Product>>> GetProductById(string id)
    {
        var product = await _repository.GetProduct(id);
        if (product != null)
            return Ok(product);

        _logger.LogError($"product with id: {id}, not found.");
        return NotFound();
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Product>>> GetProductByCategory(string category)
    {
        var products = await _repository.GetProductsByCategory(category);
        return Ok(products);
    }

    [Route("[action]/{name}", Name = "GetProductByName")]
    [HttpGet]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Product>>> GetProductByName(string name)
    {
        var products = await _repository.GetProductsByName(name);
        return Ok(products);
    }

    [HttpPost]
    [Route("[action]", Name = "CreateProduct")]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.Product>> CreateProduct([FromBody] Entities.Product product)
    {
        await _repository.CreateProduct(product);
        await _publishEndpoint.Publish(product);
        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut]
    [Route("[action]", Name = "UpdateProduct")]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Entities.Product product)
    {
        return Ok(await _repository.UpdateProduct(product));
    }

    [HttpDelete]
    [Route("[action]/{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        return Ok(await _repository.DeleteProduct(id));
    }
}