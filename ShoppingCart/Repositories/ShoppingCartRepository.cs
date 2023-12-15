using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCart.Data.Interfaces;
using ShoppingCart.Repositories.Interfaces;

namespace ShoppingCart.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IShoppingCartContext _context;
    
    public ShoppingCartRepository(IShoppingCartContext context)
    {
        _context = context;
    }
    
    public async Task CreateShoppingCart(Entities.ShoppingCart shoppingCart)
    {
        shoppingCart.Id = ObjectId.GenerateNewId().ToString();
        await _context.ShoppingCarts.InsertOneAsync(shoppingCart);
    }
    
    public async Task<bool> DeleteShoppingCart(string id)
    {
        var filter = Builders<Entities.ShoppingCart>.Filter.Eq(p => p.Id, id);
        var deleteResult = await _context.ShoppingCarts.DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
    
    public async Task<Entities.ShoppingCart> GetShoppingCart(string id)
    {
        return await _context.ShoppingCarts.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Entities.ShoppingCart>> GetShoppingCarts()
    {
        return await _context.ShoppingCarts.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<Entities.ShoppingCart>> GetShoppingCartsByCategory(string category)
    {
        var filter = Builders<Entities.ShoppingCart>.Filter.Eq(p => p.Category, category);
        return await _context.ShoppingCarts.Find(filter).ToListAsync();
    }
    
    public async Task<IEnumerable<Entities.ShoppingCart>> GetShoppingCartsByName(string name)
    {
        var filter = Builders<Entities.ShoppingCart>.Filter.Eq(p => p.Name, name);
        return await _context.ShoppingCarts.Find(filter).ToListAsync();
    }
    public async Task<bool> UpdateShoppingCart(Entities.ShoppingCart shoppingCart)
    {
        var updateresult = await _context.ShoppingCarts.ReplaceOneAsync(g => g.Id == shoppingCart.Id, shoppingCart);

        return updateresult.IsAcknowledged && updateresult.ModifiedCount > 0;
    } 
}