
namespace Shared.Models;

public class Tenant(string id, string name)
{
    public Tenant() : this(string.Empty, string.Empty)
    {
    }

    public string Id { get; set; } = id;

    public string Name { get; set; } = name;
}