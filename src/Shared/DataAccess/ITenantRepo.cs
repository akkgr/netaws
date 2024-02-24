using System.Threading.Tasks;
using Shared.Models;

namespace Shared.DataAccess;

public interface ITenantsRepo
{
    Task<Tenant?> GetTenant(string id);
}
