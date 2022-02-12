using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.Graph.Outputs;
using TwitterServer.Models;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.Services.Services
{
    public class FollowRepository : IFollowRepository
    {
        private readonly AppDbContext _dbContext;
        public FollowRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddFollow(Guid userID, Guid followId)
        {
            if (await _dbContext.TwitterUsers.FindAsync(userID) is null)
                return false;
            if (await _dbContext.TwitterUsers.FindAsync(followId) is null)
                return false;
            if (await _dbContext.FollowUser.Where(x => x.FollowerUserId == userID && x.TwitterUserId == followId).FirstOrDefaultAsync() is not null)
                return false;
            await _dbContext.FollowUser.AddAsync(new DataLayer.Models.FollowUser() { FollowerUserId = userID, TwitterUserId = followId });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<UserProfileModel>> GetAllUser(Guid userID)
        {
            return await _dbContext.TwitterUsers.Where(x => x.Id != userID).Select(x => new UserProfileModel() { Email = x.Email, Id = x.Id, FullName = x.FullName }).Take(10).ToListAsync();
        }
    }
}
