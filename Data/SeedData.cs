using WeekFive.Models;

namespace WeekFive.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
          
            context.Database.EnsureCreated();

           
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Clothing" },
                new Category { Name = "Books" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

           
            if (!context.Products.Any())
            {
                var electronicsId = context.Categories.FirstOrDefault(c => c.Name == "Electronics")?.Id ?? 0;
                var clothingId = context.Categories.FirstOrDefault(c => c.Name == "Clothing")?.Id ?? 0;
                var booksId = context.Categories.FirstOrDefault(c => c.Name == "Books")?.Id ?? 0;

                var products = new List<Product>
            {
                new Product
                {
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with 2.4GHz receiver",
                    Price = 29.99m,
                    CategoryId = electronicsId
                },
                new Product
                {
                    Name = "Cotton T-Shirt",
                    Description = "Premium quality cotton crew-neck t-shirt",
                    Price = 19.99m,
                    CategoryId = clothingId
                },
                new Product
                {
                    Name = "C# Programming Guide",
                    Description = "Comprehensive guide to C# programming",
                    Price = 49.99m,
                    CategoryId = booksId
                },
                new Product
                {
                    Name = "Bluetooth Headphones",
                    Description = "Noise-cancelling wireless headphones",
                    Price = 149.99m,
                    CategoryId = electronicsId
                }
            };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
