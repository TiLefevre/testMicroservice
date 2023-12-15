using System.Net;
using Microsoft.AspNetCore.Mvc;
using Stock.Repositories.Interfaces;

namespace Stock.Controller;

public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IStockRepository _repository;

    public StockController(IStockRepository repository, ILogger<StockController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("Stocks", Name = "GetStocks")]
    [ProducesResponseType(typeof(IEnumerable<Entities.Stock>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Stock>>> GetStocks()
    {
        var stocks = await _repository.GetStocks();
        return Ok(stocks);
    }
    
    [HttpGet("{id:length(24)}", Name = "GetStock")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.Stock), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Stock>>> GetStockById(string id)
    {
        var stock = await _repository.GetStock(id);
        if (stock != null)
            return Ok(stock);

        _logger.LogError($"Stock with id: {id}, not found.");
        return NotFound();
    }

    [Route("[action]/{productId}", Name = "GetStocksByProductId")]
    [HttpGet]
    [ProducesResponseType(typeof(Entities.Stock), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.Stock>>> GetStockByCategory(string productId)
    {
        var stocks = await _repository.GetStocksByProductId(productId);
        return Ok(stocks);
    }
    
    [HttpPost]
    [Route("[action]", Name = "CreateStock")]
    [ProducesResponseType(typeof(Entities.Stock), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.Stock>> CreateStock([FromBody] Entities.Stock Stock)
    {
        await _repository.CreateStock(Stock);
        return CreatedAtRoute("GetStock", new { id = Stock.Id }, Stock);
    }

    [HttpPut]
    [Route("[action]", Name = "UpdateStock")]
    [ProducesResponseType(typeof(Entities.Stock), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateStock([FromBody] Entities.Stock Stock)
    {
        return Ok(await _repository.UpdateStock(Stock));
    }

    [HttpDelete]
    [Route("[action]/{id:length(24)}", Name = "DeleteStock")]
    [ProducesResponseType(typeof(Entities.Stock), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteStock(string id)
    {
        return Ok(await _repository.DeleteStock(id));
    }
}