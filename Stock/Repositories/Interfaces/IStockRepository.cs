namespace Stock.Repositories.Interfaces;

public interface  IStockRepository
{
    Task<IEnumerable<Entities.Stock>> GetStocks();

    Task<Entities.Stock> GetStock(string id);

    Task<IEnumerable<Entities.Stock>> GetStocksByProductId(string productId);
    
    Task CreateStock(Entities.Stock stock);

    Task<bool> UpdateStock(Entities.Stock stock);

    Task<bool> DeleteStock(string id);
}