using MongoDB.Driver;

namespace ShoppingCart.Data.Interfaces;

public interface  IShoppingCartContext
{
    public IMongoCollection<Entities.ShoppingCart> ShoppingCarts { get; }
}