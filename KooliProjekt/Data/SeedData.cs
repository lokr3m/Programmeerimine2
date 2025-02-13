using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Data
{
    [ExcludeFromCodeCoverage]
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var user = new IdentityUser
            {
                UserName = "admin",
                Email = "admin",
                EmailConfirmed = true,
            };

            userManager.CreateAsync(user, "Password123!").Wait();

            var categories = new Category[]
            {
                new Category { Name = "Electronics", Description = "Various electronic devices" },
                new Category { Name = "Clothing", Description = "Men's and women's apparel" },
                new Category { Name = "Books", Description = "Wide range of books" },
                new Category { Name = "Home Appliances", Description = "Household appliances" },
                new Category { Name = "Toys", Description = "Children's toys" },
                new Category { Name = "Sports", Description = "Sports equipment" },
                new Category { Name = "Health", Description = "Health-related products" },
                new Category { Name = "Beauty", Description = "Beauty and skincare products" },
                new Category { Name = "Furniture", Description = "Home and office furniture" },
                new Category { Name = "Food", Description = "Groceries and food items" }
            };
            context.Categories.AddRange(categories);

            var products = new Product[]
            {
                new Product { Name = "Laptop", Description = "High performance laptop", Price = 1200m, Category = categories[0] },
                new Product { Name = "Smartphone", Description = "Latest smartphone model", Price = 800m, Category = categories[0] },
                new Product { Name = "Headphones", Description = "Noise-canceling headphones", Price = 150m, Category = categories[0] },
                new Product { Name = "Refrigerator", Description = "Double door refrigerator", Price = 500m, Category = categories[0] },
                new Product { Name = "Book - C# Programming", Description = "Learn C# programming", Price = 30m, Category = categories[0] },
                new Product { Name = "T-Shirt", Description = "Cotton T-shirt", Price = 15m, Category = categories[0] },
                new Product { Name = "Dumbbells", Description = "Set of dumbbells", Price = 50m, Category = categories[0] },
                new Product { Name = "Skincare Set", Description = "Complete skincare set", Price = 45m, Category = categories[0] },
                new Product { Name = "Sofa", Description = "Comfortable sofa", Price = 600m, Category = categories[0] },
                new Product { Name = "Organic Pasta", Description = "Healthy organic pasta", Price = 10m, Category = categories[0] }
            };
            context.Products.AddRange(products);

            var orders = new Order[]
            {
                new Order { OrderDate = DateTime.Now, TotalAmount = 500m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 750m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 120m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 600m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 90m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 350m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 200m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 430m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 700m, Status = "Pending", User = user },
                new Order { OrderDate = DateTime.Now, TotalAmount = 85m, Status = "Pending", User = user }
            };
            context.Orders.AddRange(orders);

            var orderProducts = new OrderProduct[]
            {
                new OrderProduct { Order = orders[0], Product = products[0], Quantity = 1 },
                new OrderProduct { Order = orders[1], Product = products[1], Quantity = 2 },
                new OrderProduct { Order = orders[2], Product = products[2], Quantity = 1 },
                new OrderProduct { Order = orders[3], Product = products[3], Quantity = 1 },
                new OrderProduct { Order = orders[4], Product = products[4], Quantity = 3 },
                new OrderProduct { Order = orders[5], Product = products[5], Quantity = 2 },
                new OrderProduct { Order = orders[6], Product = products[6], Quantity = 4 },
                new OrderProduct { Order = orders[7], Product = products[7], Quantity = 1 },
                new OrderProduct { Order = orders[8], Product = products[8], Quantity = 1 },
                new OrderProduct { Order = orders[9], Product = products[9], Quantity = 5 }
            };
            context.OrderProducts.AddRange(orderProducts);

            var shoppingCarts = new ShoppingCart[]
            {
                new ShoppingCart { TotalQuantity = 3, TotalPrice = 150m },
                new ShoppingCart { TotalQuantity = 5, TotalPrice = 300m },
                new ShoppingCart { TotalQuantity = 2, TotalPrice = 90m },
                new ShoppingCart { TotalQuantity = 4, TotalPrice = 220m },
                new ShoppingCart { TotalQuantity = 6, TotalPrice = 450m },
                new ShoppingCart { TotalQuantity = 1, TotalPrice = 50m },
                new ShoppingCart { TotalQuantity = 8, TotalPrice = 500m },
                new ShoppingCart { TotalQuantity = 7, TotalPrice = 350m },
                new ShoppingCart { TotalQuantity = 5, TotalPrice = 260m },
                new ShoppingCart { TotalQuantity = 10, TotalPrice = 700m }
            };
            context.ShoppingCarts.AddRange(shoppingCarts);

            var cartProducts = new CartProduct[]
            {
                new CartProduct { ShoppingCart = shoppingCarts[0], Product = products[0], Quantity = 1 },
                new CartProduct { ShoppingCart = shoppingCarts[1], Product = products[1], Quantity = 2 },
                new CartProduct { ShoppingCart = shoppingCarts[2], Product = products[2], Quantity = 1 },
                new CartProduct { ShoppingCart = shoppingCarts[3], Product = products[3], Quantity = 1 },
                new CartProduct { ShoppingCart = shoppingCarts[4], Product = products[4], Quantity = 3 },
                new CartProduct { ShoppingCart = shoppingCarts[5], Product = products[5], Quantity = 2 },
                new CartProduct { ShoppingCart = shoppingCarts[6], Product = products[6], Quantity = 4 },
                new CartProduct { ShoppingCart = shoppingCarts[7], Product = products[7], Quantity = 1 },
                new CartProduct { ShoppingCart = shoppingCarts[8], Product = products[8], Quantity = 1 },
                new CartProduct { ShoppingCart = shoppingCarts[9], Product = products[9], Quantity = 5 }
            };
            context.CartProducts.AddRange(cartProducts);

            context.SaveChanges();
        }
    }
}
