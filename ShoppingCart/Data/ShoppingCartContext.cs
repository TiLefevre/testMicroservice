using MongoDB.Driver;
using ShoppingCart.Data.Interfaces;

namespace ShoppingCart.Data;

public class ShoppingCartContext : IShoppingCartContext
{
    public ShoppingCartContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        ShoppingCarts = database.GetCollection<Entities.ShoppingCart>(
            configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        ShoppingCartContextSeed.SeedData(ShoppingCarts);
    }
    public IMongoCollection<Entities.ShoppingCart> ShoppingCarts { get; }
}