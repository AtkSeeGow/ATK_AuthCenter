using Duende.IdentityServer.Models;

namespace AuthCenter.Service
{
    public class ApiResourceService
    {
        public List<ApiResource> Get()
        {
            var result = new List<ApiResource>();
            result.Add(new ApiResource("api1"));
            return result;
        }
    }
}

