using Duende.IdentityServer.Models;

namespace AuthCenter.Service
{
    public class ClientService
    {
        public List<Client> Get()
        {
            var result = new List<Client>()
            {
                new Client
                {
                    ClientId = "angular-template",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {
                        "http://localhost:4200/callback"
                    },
                    AllowedScopes = { "openid", "profile", "api1" },
                    RequirePkce = false,
                    Enabled = true,  // 
                    RequireClientSecret = false
                }
            };
            return result;
        }
    }
}

