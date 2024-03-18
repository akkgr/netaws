
namespace Shared.Models;

public class TenantQuery(string id)
{
    public TenantQuery() : this(string.Empty)
    {
    }

    public string Id { get; set; } = id;
}