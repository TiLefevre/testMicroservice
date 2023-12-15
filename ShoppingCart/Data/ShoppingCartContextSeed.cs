using MongoDB.Driver;

namespace ShoppingCart.Data;

public class ShoppingCartContextSeed
{
    public static void SeedData(IMongoCollection<Entities.ShoppingCart> ShoppingCartCollection)
    {
        var existShoppingCart = ShoppingCartCollection.Find(p => true).Any();
        if(!existShoppingCart) ShoppingCartCollection.InsertMany(GetPreconfiguredShoppingCarts());
    }

    private static IEnumerable<Entities.ShoppingCart> GetPreconfiguredShoppingCarts()
    {
        return new List<Entities.ShoppingCart>
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