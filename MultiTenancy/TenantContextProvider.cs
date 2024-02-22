namespace EventSourcing.MultiTenancy;

public sealed class TenantContextProvider
{
    public TenantContext? Current { get; private set; }

    public void Initialize(string tenantIdentifier)
    {
        Current = new TenantContext(tenantIdentifier);
    }
}

public record TenantContext(string Identifier);