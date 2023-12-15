using MongoDB.Driver;
using Product.Data.Interfaces;

namespace Product.Data;

public class ProductContext : IProductContext
{
    public ProductContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Products = database.GetCollection<Entities.Product>(
            configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        ProductContextSeed.SeedData(Products);
    }
    public IMongoCollection<Entities.Product> Products { get; }
}