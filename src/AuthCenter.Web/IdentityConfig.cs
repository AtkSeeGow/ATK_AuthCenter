using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

public static class IdentityConfig
{
    public static List<Client> Clients => new()
    {
        new Client
        {
            ClientId = "accounting-system",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = { "https://accounting.com/callback" },
            AllowedScopes = { "openid", "profile", "api1" }
        },
        new Client
        {
            ClientId = "angular-client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // ¤ä´© Password Flow
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "openid", "profile", "api1" }
        }
    };

    public static List<ApiScope> ApiScopes => new()
    {
        new ApiScope("api1", "My API")
    };

    public static List<ApiResource> ApiResources => new()
    {
        new ApiResource("api1")
    };

    public static List<IdentityResource> IdentityResources => new()
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static List<TestUser> Users => new()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "admin",
            Password = "1234"
        }
    };
}