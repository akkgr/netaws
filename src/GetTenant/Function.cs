using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
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
    [HttpApi(LambdaHttpMethod.Get, "/{id}")]
    public async Task<Tenant> FunctionHandler(string id)
    {
        return await _dataAccess.GetTenant(id) ?? throw new ResourceNotFoundException($"Tenant with id {id} not found");
    }
}