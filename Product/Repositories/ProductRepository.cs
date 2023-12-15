using MongoDB.Bson;
using MongoDB.Driver;
using Product.Data.Interfaces;
using Product.Repositories.Interfaces;

namespace Product.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IProductContext _context;
    
    public ProductRepository(IProductContext context)
    {
        _context = context;
    }
    
    public async Task CreateProduct(Entities.Product product)
    {
        product.Id = ObjectId.GenerateNewId().ToString();
        await _context.Products.InsertOneAsync(product);
    }
    
    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Id, id);
        var deleteResult = await _context.Products.DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
    
    public async Task<Entities.Product> GetProduct(string id)
    {
        return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Entities.Product>> GetProducts()
    {
        return await _context.Products.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<Entities.Product>> GetProductsByCategory(string category)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Category, category);
        return await _context.Products.Find(filter).ToListAsync();
    }
    
    public async Task<IEnumerable<Entities.Product>> GetProductsByName(string name)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Name, name);
        return await _context.Products.Find(filter).ToListAsync();
    }
    public async Task<bool> UpdateProduct(Entities.Product product)
    {
        var updateresult = await _context.Products.ReplaceOneAsync(g => g.Id == product.Id, product);

        return updateresult.IsAcknowledged && updateresult.ModifiedCount > 0;
    } 
}