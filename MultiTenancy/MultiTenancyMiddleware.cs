namespace EventSourcing.MultiTenancy;

public sealed class MultiTenancyMiddleware(TenantContextProvider tenantContextProvider) : IMiddleware
{
    private readonly TenantContextProvider _tenantContextProvider = tenantContextProvider;

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        var tenantIdentifier = httpContext
            .Request
            .Headers["Tenant"];

        // Tenant identifier from HTTP request would be passed into this method
        _tenantContextProvider.Initialize();

        await next(httpContext);
    }
}