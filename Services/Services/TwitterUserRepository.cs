using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Graph.Inputs;
using TwitterServer.Auth;
using TwitterServer.Data;
using TwitterServer.DataLayer.Models;
using TwitterServer.Graph.Outputs;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.Services.Services
{
    public class TwitterUserRepository : ITwitterUserRepository
    {
        private readonly IIdentityService _identityService;
        private readonly AppDbContext _dbContext;
        public TwitterUserRepository(AppDbContext dbContext, IIdentityService identityService)
        {
            _dbContext = dbContext;
            _identityService = identityService; 
        }

        public async Task<bool> RegisterTwitterUser(TwitterUser TwitterUser)
        {
            try
            {
                if (TwitterUser == null) return false;
                if (string.IsNullOrEmpty(TwitterUser.Email) || string.IsNullOrEmpty(TwitterUser.UserName) || string.IsNullOrEmpty(TwitterUser.FullName) || string.IsNullOrEmpty(TwitterUser.Password)) return false;
                await _dbContext.TwitterUsers.AddAsync(TwitterUser);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return
                    false; ;
            }
        }
        public async Task<LoginResultOutput> LoginTwitterUser(LoginInput input)
        {
            var user = await _dbContext.TwitterUsers.Where(z =>( z.UserName == input.username || z.Email == input.username) && z.Password == input.password).FirstOrDefaultAsync();
            if (user == null) return new LoginResultOutput(false, "auth error!", null);
            var result = await _identityService.Authenticate(user.Email, user.Id.ToString());
            return new LoginResultOutput(true, "login success!", result);
        }
    }
}
