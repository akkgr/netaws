using System.Threading;
using System.Threading.Tasks;

namespace Tenants.Data.DynamoDb;

public interface ITenantsRepository
{
    Task<TenantDao?> GetTenant(string id, CancellationToken token);
}