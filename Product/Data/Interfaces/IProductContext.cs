using MongoDB.Driver;

namespace Product.Data.Interfaces;

public interface  IProductContext
{
    public IMongoCollection<Entities.Product> Products { get; }
}