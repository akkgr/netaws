using System;
using System.Threading;
using System.Threading.Tasks;

using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

using GetTenant;

using Shared.DataAccess;
using Shared.Models;


[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<CustomJsonSerializerContext>))]

namespace GetTenant;

public class Function
{
    private readonly ITenantsRepo _dataAccess;

    public Function(ITenantsRepo dataAccess)
    {
        this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
    }

    [LambdaFunction]
    public async Task<Tenant> FunctionHandler(TenantQuery query, ILambdaContext context)
    {
        var cts = new CancellationTokenSource(context.RemainingTime);
        return await _dataAccess.GetTenant(query.Id, cts.Token) ??
            throw new ResourceNotFoundException($"Tenant with id {query.Id} not found");
    }
}