using MongoDB.Bson;
using MongoDB.Driver;
using Stock.Data.Interfaces;
using Stock.Repositories.Interfaces;

namespace Stock.Repositories;

public class StockRepository : IStockRepository
{
    private readonly IStockContext _context;
    
    public StockRepository(IStockContext context)
    {
        _context = context;
    }
    
    public async Task CreateStock(Entities.Stock stock)
    {
        stock.Id = ObjectId.GenerateNewId().ToString();
        await _context.Stocks.InsertOneAsync(stock);
    }
    
    public async Task<bool> DeleteStock(string id)
    {
        var filter = Builders<Entities.Stock>.Filter.Eq(p => p.Id, id);
        var deleteResult = await _context.Stocks.DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
    
    public async Task<Entities.Stock> GetStock(string id)
    {
        return await _context.Stocks.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Entities.Stock>> GetStocks()
    {
        return await _context.Stocks.Find(p => true).ToListAsync();
    }
    
    public async Task<IEnumerable<Entities.Stock>> GetStocksByProductId(string productId)
    {
        var filter = Builders<Entities.Stock>.Filter.Eq(p => p.ProductId, productId);
        return await _context.Stocks.Find(filter).ToListAsync();
    }
    public async Task<bool> UpdateStock(Entities.Stock stock)
    {
        var updateresult = await _context.Stocks.ReplaceOneAsync(g => g.Id == stock.Id, stock);

        return updateresult.IsAcknowledged && updateresult.ModifiedCount > 0;
    } 
}