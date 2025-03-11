using Duende.IdentityServer.Models;

namespace AuthCenter.Service
{
    public class IdentityResourceService
    {
        public List<IdentityResource> Get()
        {
            var result = new List<IdentityResource>();
            result.Add(new IdentityResources.OpenId());
            result.Add(new IdentityResources.Profile());
            return result;
        }
    }
}

