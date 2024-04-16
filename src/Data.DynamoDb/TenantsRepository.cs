using System;
using System.Threading;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Tenants.Data.DynamoDb;

public class TenantsRepository(IAmazonDynamoDB dynamoDbClient) : ITenantsRepository
{
    private static readonly string ProductTableName = "products";
    private readonly IAmazonDynamoDB _dynamoDbClient = dynamoDbClient ?? throw new ArgumentNullException(nameof(dynamoDbClient));

    public async Task<TenantDao?> GetTenant(string id, CancellationToken token)
    {
        var config = new DynamoDBOperationConfig
        {
            OverrideTableName = ProductTableName
        };
        var context = new DynamoDBContext(_dynamoDbClient);
        var model = await context.LoadAsync<TenantDao>(id, config, token);

        return model;
    }
}