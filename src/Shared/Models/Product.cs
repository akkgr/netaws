using System;

namespace Shared.Models
{
    public class Product(string id, string name, decimal price)
    {
        public Product() : this(string.Empty, string.Empty, 0)
        {
        }

        public string Id { get; set; } = id;

        public string Name { get; set; } = name;

        public decimal Price { get; private set; } = price;

        public void SetPrice(decimal newPrice)
        {
            this.Price = Math.Round(newPrice, 2);
        }

        public override string ToString()
        {
            return "Product{" +
                   "id='" + this.Id + '\'' +
                   ", name='" + this.Name + '\'' +
                   ", price=" + this.Price +
                   '}';
        }
    }
}