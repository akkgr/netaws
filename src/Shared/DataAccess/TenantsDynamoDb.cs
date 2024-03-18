using System;
using System.Threading;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

using Shared.Models;

namespace Shared.DataAccess;

public class TenantsDynamoDb(IAmazonDynamoDB dynamoDbClient) : ITenantsRepo
{
    private static readonly string ProductTableName = "products";
    private readonly IAmazonDynamoDB _dynamoDbClient = dynamoDbClient ?? throw new ArgumentNullException(nameof(dynamoDbClient));

    public async Task<Tenant?> GetTenant(string id, CancellationToken token)
    {
        var config = new DynamoDBOperationConfig
        {
            OverrideTableName = ProductTableName
        };
        var context = new DynamoDBContext(_dynamoDbClient);
        var model = await context.LoadAsync<Tenant>(id, config, token);

        return model;
    }
}