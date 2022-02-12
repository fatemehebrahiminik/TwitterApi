using System.Threading.Tasks;

namespace TwitterServer.Auth
{
    public interface IIdentityService
    {
        Task<string> Authenticate(string email, string userId);
    }
}
