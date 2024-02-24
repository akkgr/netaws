using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.DataAccess
{
    public class DynamoDbProducts : IProductsDao
    {
        private static readonly string ProductTableName = "products";
        private readonly AmazonDynamoDBClient _dynamoDbClient = new();

        public async Task<Product?> GetProduct(string id)
        {
            var getItemResponse = await this._dynamoDbClient.GetItemAsync(new GetItemRequest(ProductTableName,
                new Dictionary<string, AttributeValue>(1)
                {
                    {ProductMapper.Pk, new AttributeValue(id)}
                }));

            return getItemResponse.IsItemSet ? ProductMapper.ProductFromDynamoDb(getItemResponse.Item) : null;
        }

        public async Task PutProduct(Product product)
        {
            await this._dynamoDbClient.PutItemAsync(ProductTableName, ProductMapper.ProductToDynamoDb(product));
        }

        public async Task DeleteProduct(string id)
        {
            await this._dynamoDbClient.DeleteItemAsync(ProductTableName, new Dictionary<string, AttributeValue>(1)
            {
                {ProductMapper.Pk, new AttributeValue(id)}
            });
        }

        public async Task<ProductWrapper> GetAllProducts()
        {
            var data = await this._dynamoDbClient.ScanAsync(new ScanRequest()
            {
                TableName = ProductTableName,
                Limit = 20
            });

            var products = new List<Product>();

            foreach (var item in data.Items)
            {
                products.Add(ProductMapper.ProductFromDynamoDb(item));
            }

            return new ProductWrapper(products);
        }
    }
}