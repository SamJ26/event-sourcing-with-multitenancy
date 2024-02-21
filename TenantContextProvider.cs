namespace EventSourcing;

public sealed class TenantContextProvider
{
    // In real application, this value would be set in the middleware
    public TenantContext Current => new TenantContext("Google", "tenant_google");
}

public record TenantContext(
    string Name,
    string Database);