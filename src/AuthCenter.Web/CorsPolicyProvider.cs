using Duende.IdentityServer.Services;

public class CorsPolicyProvider : ICorsPolicyService
{
    public Task<bool> IsOriginAllowedAsync(string origin)
    {
        // Allow localhost:4200 (your Angular app)
        return Task.FromResult(origin == "http://localhost:4200");
    }
}