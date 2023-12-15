using MongoDB.Driver;

namespace Stock.Data;

public class StockContextSeed
{
    public static void SeedData(IMongoCollection<Entities.Stock> StockCollection)
    {
        var existStock = StockCollection.Find(p => true).Any();
        if(!existStock) StockCollection.InsertMany(GetPreconfiguredStocks());
    }

    private static IEnumerable<Entities.Stock> GetPreconfiguredStocks()
    {
        return new List<Entities.Stock>
        {
            new()
            {
                Id = "602d2149e773f2a3990b47f5",
                ProductId = "602d2149e773f2a3990b47f5",
                Quantity = 15
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f6",
                ProductId = "602d2149e773f2a3990b47f6",
                Quantity = 45
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f7",
                ProductId = "602d2149e773f2a3990b47f7",
                Quantity = 32
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f8",
                ProductId = "602d2149e773f2a3990b47f8",
                Quantity = 12
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f9",
                ProductId = "602d2149e773f2a3990b47f9",
                Quantity = 87
            }
        };
    }
}