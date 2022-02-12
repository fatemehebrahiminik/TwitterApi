using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterServer.DataLayer.Models;
using TwitterServer.Graph.Outputs;
using TwitterServer.Models;

namespace TwitterServer.Services.Interfaces
{
    public interface IFollowRepository
    {
        public Task<bool> AddFollow(Guid userID, Guid followId);
        public Task<List<UserProfileModel>> GetAllUser(Guid userID);
    }
}
