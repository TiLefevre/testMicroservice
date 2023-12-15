using MongoDB.Driver;

namespace Product.Data;

public class ProductContextSeed
{
    public static void SeedData(IMongoCollection<Entities.Product> productCollection)
    {
        var existProduct = productCollection.Find(p => true).Any();
        if(!existProduct) productCollection.InsertMany(GetPreconfiguredProducts());
    }

    private static IEnumerable<Entities.Product> GetPreconfiguredProducts()
    {
        return new List<Entities.Product>
        {
            new()
            {
                Id = "602d2149e773f2a3990b47f5",
                Name = "Volailles aux épices de Noël",
                Category = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation",
                Price = 25
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f6",
                Name = "Flammekueche alsacienne",
                Category = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation",
                Price = 25
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f7",
                Name = "Patates douces farcies végétariennes de Noël",
                Category = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation",
                Price = 25
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f8",
                Name = "Salade de quinoa à la feta",
                Category = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation",
                Price = 25
            },
            new()
            {
                Id = "602d2149e773f2a3990b47f9",
                Name = "Quiche au comté et aux oignons",
                Category = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation",
                Price = 25
            },
        };
    }
}