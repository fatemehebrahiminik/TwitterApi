using System.Threading.Tasks;
using Twitter.Graph.Inputs;
using TwitterServer.DataLayer.Models;
using TwitterServer.Graph.Outputs;

namespace TwitterServer.Services.Interfaces
{
    public interface ITwitterUserRepository
    {
        public Task<bool> RegisterTwitterUser(TwitterUser TwitterUser);
        public Task<LoginResultOutput> LoginTwitterUser(LoginInput input);
    }
}
