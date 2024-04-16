using Amazon.Lambda.Annotations;

using Microsoft.Extensions.DependencyInjection;

using Tenants.Data.DynamoDb;

namespace GetTenant;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDynamoDb();
    }
}