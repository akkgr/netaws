using System.Globalization;
using Amazon.DynamoDBv2.Model;
using Shared.Models;
using System.Collections.Generic;
using System;

namespace Shared.DataAccess
{
    public static class ProductMapper
    {
        public const string Pk = "id";
        private const string Name = "name";
        private const string Price = "price";

        public static Product ProductFromDynamoDb(Dictionary<String, AttributeValue> items) {
            var product = new Product(items[Pk].S, items[Name].S, decimal.Parse(items[Price].N));

            return product;
        }
        
        public static Dictionary<String, AttributeValue> ProductToDynamoDb(Product product) {
            Dictionary<String, AttributeValue> item = new Dictionary<string, AttributeValue>(3)
            {
                { Pk, new AttributeValue(product.Id) },
                { Name, new AttributeValue(product.Name) },
                { Price, new AttributeValue()
                {
                    N = product.Price.ToString(CultureInfo.InvariantCulture)
                } }
            };

            return item;
        }
    }
}