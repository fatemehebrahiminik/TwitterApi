using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterServer.DataLayer.Models;
using TwitterServer.Graph.Inputs;
using TwitterServer.Graph.Outputs;
using TwitterServer.Models;

namespace TwitterServer.Services.Interfaces
{
    public interface ITwitteRepository
    {
        public Task<TwitteResultOutput> CreateTwitte(CreateTwitteInput input);
        public Task<TwitteListResultOutput> GetUserTwittes(Guid id);
        public Task<TwitteResultOutput> CreateReTwitte(CreateReTwitteInput input);
        public Task<UserProfileModel> GetUserProfile(Guid userId);
    }
}
