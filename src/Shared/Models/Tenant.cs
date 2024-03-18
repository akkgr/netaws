
using Amazon.DynamoDBv2.DataModel;

namespace Shared.Models;

public class Tenant(string id, string name)
{
    public Tenant() : this(string.Empty, string.Empty)
    {
    }

    [DynamoDBHashKey("id")]
    public string Id { get; set; } = id;

    [DynamoDBProperty("name")]
    public string Name { get; set; } = name;
}