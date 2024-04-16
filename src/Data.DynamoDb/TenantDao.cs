
using Amazon.DynamoDBv2.DataModel;

namespace Tenants.Data.DynamoDb;

public class TenantDao
{
    [DynamoDBHashKey("id")]
    public string Id { get; set; } = string.Empty;

    [DynamoDBProperty("name")]
    public string Name { get; set; } = string.Empty;
}