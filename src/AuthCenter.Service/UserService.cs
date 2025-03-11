using Duende.IdentityServer.Test;

namespace AuthCenter.Service
{
    public class UserService
    {
        public static List<TestUser> Users { get; set; } = new List<TestUser>()
        {
            new TestUser
            {
                SubjectId = "admin",
                Username = "admin",
                Password = "1234"
            }
        };
    }
}

