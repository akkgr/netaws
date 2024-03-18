using System.Threading;
using System.Threading.Tasks;

using Shared.Models;

namespace Shared.DataAccess;

public interface ITenantsRepo
{
    Task<Tenant?> GetTenant(string id, CancellationToken token);
}