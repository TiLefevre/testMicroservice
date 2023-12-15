using MongoDB.Driver;

namespace Stock.Data.Interfaces;

public interface  IStockContext
{
    public IMongoCollection<Entities.Stock> Stocks { get; }
}