using Duende.IdentityServer.Models;

namespace AuthCenter.Service
{
    public class ApiScopeService
    {
        public List<ApiScope> Get()
        {
            var result = new List<ApiScope>();
            result.Add(new ApiScope("api1", "My API"));
            return result;
        }
    }
}

