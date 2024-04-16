using System;
using System.Threading;
using System.Threading.Tasks;

using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

using GetTenant;

using Tenants.Abstractions.Models;
using Tenants.Data.DynamoDb;


[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<CustomJsonSerializerContext>))]

namespace GetTenant;

public class Function
{
    private readonly ITenantsRepository _dataAccess;

    public Function(ITenantsRepository dataAccess)
    {
        this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
    }

    [LambdaFunction]
    public async Task<Tenant> FunctionHandler(TenantQuery query, ILambdaContext context)
    {
        var cts = new CancellationTokenSource(context.RemainingTime);
        var dao = await _dataAccess.GetTenant(query.Id, cts.Token);

        if (dao == null)
        {
            throw new ResourceNotFoundException($"Tenant with id {query.Id} not found");
        }

        return new Tenant
        {
            Id = dao.Id,
            Name = dao.Name,
        };
    }
}