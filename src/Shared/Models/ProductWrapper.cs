using System.Collections.Generic;

namespace Shared.Models
{
    public class ProductWrapper(List<Product> products)
    {
        public ProductWrapper() : this(new List<Product>())
        {
        }

        public List<Product> Products { get; set; } = products;
    }
}