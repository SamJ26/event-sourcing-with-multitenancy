namespace EventSourcing;

public sealed class TenantContextProvider
{
    public TenantContext Current => new TenantContext("Google", "tenant_google");
}

public record TenantContext(
    string Name,
    string Database);