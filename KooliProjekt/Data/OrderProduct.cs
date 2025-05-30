﻿using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Data
{
    [ExcludeFromCodeCoverage]
    public class OrderProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public Product Product { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}
