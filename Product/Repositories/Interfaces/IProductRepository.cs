namespace Product.Repositories.Interfaces;

public interface  IProductRepository
{
    Task<IEnumerable<Entities.Product>> GetProducts();

    Task<Entities.Product> GetProduct(string id);

    Task<IEnumerable<Entities.Product>> GetProductsByName(string name);

    Task<IEnumerable<Entities.Product>> GetProductsByCategory(string category);

    Task CreateProduct(Entities.Product product);

    Task<bool> UpdateProduct(Entities.Product product);

    Task<bool> DeleteProduct(string id);
}