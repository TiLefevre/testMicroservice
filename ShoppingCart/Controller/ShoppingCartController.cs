using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Repositories.Interfaces;

namespace ShoppingCart.Controller;

public class ShoppingCartController : ControllerBase
{
    private readonly ILogger<ShoppingCartController> _logger;
    private readonly IShoppingCartRepository _repository;

    public ShoppingCartController(IShoppingCartRepository repository, ILogger<ShoppingCartController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("ShoppingCarts", Name = "GetShoppingCarts")]
    [ProducesResponseType(typeof(IEnumerable<Entities.ShoppingCart>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.ShoppingCart>>> GetShoppingCarts()
    {
        var ShoppingCarts = await _repository.GetShoppingCarts();
        return Ok(ShoppingCarts);
    }
    
    [HttpGet("{id:length(24)}", Name = "GetShoppingCart")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.ShoppingCart>>> GetShoppingCartById(string id)
    {
        var ShoppingCart = await _repository.GetShoppingCart(id);
        if (ShoppingCart != null)
            return Ok(ShoppingCart);

        _logger.LogError($"ShoppingCart with id: {id}, not found.");
        return NotFound();
    }

    [Route("[action]/{category}", Name = "GetShoppingCartByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.ShoppingCart>>> GetShoppingCartByCategory(string category)
    {
        var ShoppingCarts = await _repository.GetShoppingCartsByCategory(category);
        return Ok(ShoppingCarts);
    }

    [Route("[action]/{name}", Name = "GetShoppingCartByName")]
    [HttpGet]
    [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.ShoppingCart>>> GetShoppingCartByName(string name)
    {
        var ShoppingCarts = await _repository.GetShoppingCartsByName(name);
        return Ok(ShoppingCarts);
    }

    [HttpPost]
    [Route("[action]", Name = "CreateShoppingCart")]
    [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.ShoppingCart>> CreateShoppingCart([FromBody] Entities.ShoppingCart ShoppingCart)
    {
        await _repository.CreateShoppingCart(ShoppingCart);
        return CreatedAtRoute("GetShoppingCart", new { id = ShoppingCart.Id }, ShoppingCart);
    }

    [HttpPut]
    [Route("[action]", Name = "UpdateShoppingCart")]
    [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateShoppingCart([FromBody] Entities.ShoppingCart ShoppingCart)
    {
        return Ok(await _repository.UpdateShoppingCart(ShoppingCart));
    }

    [HttpDelete]
    [Route("[action]/{id:length(24)}", Name = "DeleteShoppingCart")]
    [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteShoppingCart(string id)
    {
        return Ok(await _repository.DeleteShoppingCart(id));
    }
}