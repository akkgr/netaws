using System;
using System.Threading.Tasks;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Shared.DataAccess;
using Shared.Models;

namespace GetTenant;

public class Function
{
    private readonly IProductsDao _dataAccess;

    public Function(IProductsDao dataAccess)
    {
        this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/")]
    public async Task<ProductWrapper> FunctionHandler()
    {
        return await _dataAccess.GetAllProducts();
    }
}