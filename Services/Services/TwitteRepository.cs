using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.DataLayer.Models;
using TwitterServer.Graph.Inputs;
using TwitterServer.Graph.Outputs;
using TwitterServer.Models;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.Services.Services
{
    public class TwitteRepository : ITwitteRepository
    {
        private readonly AppDbContext _dbContext;
        public TwitteRepository(AppDbContext dbContextFactory)
        {
            _dbContext = dbContextFactory;
        }
        public async Task<TwitteResultOutput> CreateTwitte(CreateTwitteInput input)
        {
            var user = await _dbContext.TwitterUsers.FindAsync(input.AuterId);
            if (user == null) return new TwitteResultOutput(false, null);
            var twitte = new Twitte() { CreateDatetime = DateTime.Now, TwitterUserId = input.AuterId, TwitteText = input.TwitteText };
            await _dbContext.Twittes.AddAsync(twitte);
            await _dbContext.SaveChangesAsync();
            return new TwitteResultOutput(true, twitte);
        }
        public async Task<TwitteListResultOutput> GetUserTwittes(Guid id)
        {
            var user = await _dbContext.TwitterUsers.Include(x => x.Twittes).AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null) return new TwitteListResultOutput(false, null);
            return new TwitteListResultOutput(true, user.Twittes);
        }
        public async Task<TwitteResultOutput> CreateReTwitte(CreateReTwitteInput input)
        {
            var user = await _dbContext.TwitterUsers.FindAsync(input.UserId);
            if (user == null) return new TwitteResultOutput(false, null);//user not found
            var twitte = await _dbContext.Twittes.FindAsync(input.TwitteId);
            if (twitte == null) return new TwitteResultOutput(false, null);//twite not found
            var Retwitte = new Retweet() { RetweetDatetime = DateTime.Now, TwitteId = input.TwitteId, RetweetUserId = input.UserId };
            await _dbContext.Retweets.AddAsync(Retwitte);
            await _dbContext.SaveChangesAsync();
            return new TwitteResultOutput(true, twitte);
        }
        public async Task<UserProfileModel> GetUserProfile(Guid userId)
        {
            var user = await _dbContext.TwitterUsers.Include(x => x.Followers).Include(x => x.Twittes).Include(x => x.Following).AsNoTracking().Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null) return null;//user not found
            var ReTwittes = await _dbContext.Retweets.Where(x => x.RetweetUserId == user.Id).Select(x => x.Twitte).ToListAsync();
            var followings = await _dbContext.FollowUser.Where(x => x.FollowerUserId == userId).CountAsync();
            return new UserProfileModel()
            {
                Email = user.Email,
                FollowerCount = user.Followers.Count,
                FollowingCount = followings,
                FullName = user.FullName,
                Twittes = user.Twittes.Select(x => new TwitteModel() { Id = x.Id, TextTwite = x.TwitteText }).ToList(),
                ReTwittes = ReTwittes.Select(x => new TwitteModel() { Id = x.Id, TextTwite = x.TwitteText }).ToList()
            };

        }
    }
}
