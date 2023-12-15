namespace ShoppingCart.Repositories.Interfaces;

public interface  IShoppingCartRepository
{
    Task<IEnumerable<Entities.ShoppingCart>> GetShoppingCarts();

    Task<Entities.ShoppingCart> GetShoppingCart(string id);

    Task<IEnumerable<Entities.ShoppingCart>> GetShoppingCartsByName(string name);

    Task<IEnumerable<Entities.ShoppingCart>> GetShoppingCartsByCategory(string category);

    Task CreateShoppingCart(Entities.ShoppingCart ShoppingCart);

    Task<bool> UpdateShoppingCart(Entities.ShoppingCart ShoppingCart);

    Task<bool> DeleteShoppingCart(string id);
}