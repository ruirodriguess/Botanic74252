using RuiRumos74252.Models;

namespace RuiRumos74252.Data
{
    public class AppDbInitializer
    {
        public static void Seed (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // Add data - Product

                if (!context.Product.Any())
                {
                    context.Product.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Planta 1",
                            Price = 2.15,
                            Picture = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first plant"
                        },
                        new Product()
                        {
                            Name = "Planta 2",
                            Price = 3.15,
                            Picture = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the second plant"
                        },
                        new Product()
                        {
                            Name = "Planta 3",
                            Price = 4.15,
                            Picture = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the third plant"
                        },
                        new Product()
                        {
                            Name = "Planta 4",
                            Price = 5.15,
                            Picture = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the fourth plant"
                        },
                        new Product()
                        {
                            Name = "Planta 5",
                            Price = 6.15,
                            Picture = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the fifth plant"
                        },
                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
