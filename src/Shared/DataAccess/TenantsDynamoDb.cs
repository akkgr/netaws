using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using Shared.Models;

namespace Shared.DataAccess;

public class TenantsDynamoDb(IAmazonDynamoDB dynamoDbClient) : ITenantsRepo
{
    private static readonly string ProductTableName = "tenants";
    private readonly IAmazonDynamoDB _dynamoDbClient = dynamoDbClient ?? throw new ArgumentNullException(nameof(dynamoDbClient));

    public async Task<Tenant?> GetTenant(string id)
    {
        var getItemResponse = await this._dynamoDbClient.GetItemAsync(new GetItemRequest(ProductTableName,
            new Dictionary<string, AttributeValue>(1)
            {
                    {TenantMapper.Pk, new AttributeValue(id)}
            }));

        return getItemResponse.IsItemSet ? TenantMapper.TenantFromDynamoDb(getItemResponse.Item) : null;
    }
}