namespace EventSourcing.MultiTenancy;

public sealed class MultiTenancyMiddleware(TenantContextProvider tenantContextProvider) : IMiddleware
{
    private readonly TenantContextProvider _tenantContextProvider = tenantContextProvider;

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        var tenantIdentifier = httpContext
            .Request
            .Query["tenant"];

        _tenantContextProvider.Initialize(tenantIdentifier!);

        await next(httpContext);
    }
}