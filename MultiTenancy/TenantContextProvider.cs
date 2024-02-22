namespace EventSourcing.MultiTenancy;

public sealed class TenantContextProvider
{
    public TenantContext? Current { get; private set; }

    public void Initialize()
    {
        // In real application, this value would not be hardcoded
        Current = new TenantContext("Google", "tenant_google");
    }
}

public record TenantContext(
    string Name,
    string Database);