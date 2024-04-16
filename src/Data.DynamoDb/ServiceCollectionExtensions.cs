
using Amazon.DynamoDBv2;

using Microsoft.Extensions.DependencyInjection;

namespace Tenants.Data.DynamoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamoDb(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient());
        services.AddSingleton<ITenantsRepository, TenantsRepository>();

        return services;
    }
}