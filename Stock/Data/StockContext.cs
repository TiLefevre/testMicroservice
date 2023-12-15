using MongoDB.Driver;
using Stock.Data.Interfaces;

namespace Stock.Data;

public class StockContext : IStockContext
{
    public StockContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Stocks = database.GetCollection<Entities.Stock>(
            configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        StockContextSeed.SeedData(Stocks);
    }
    public IMongoCollection<Entities.Stock> Stocks { get; }
}