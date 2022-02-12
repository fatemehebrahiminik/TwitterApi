using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Twitter.Graph.Inputs;
using TwitterServer.Auth;
using TwitterServer.Graph.Outputs;
using TwitterServer.Models;
using TwitterServer.Services.Interfaces;
using TwitterServer.Services.Services;

namespace TwitterServer.GraphQL
{
    public class Query
    { 
        private readonly ITwitteRepository _twitteRepository;
        private readonly IFollowRepository _followRepository;
         
        public Query(ITwitteRepository twitteRepository, IFollowRepository followRepository)
        { 
            _twitteRepository = twitteRepository;
            _followRepository= followRepository;
        } 
        [Authorize]
        public async Task<TwitteListResultOutput> GetAllUserTwittes([CurrentUserGlobalState] CurrentUser user)
        {
            return await _twitteRepository.GetUserTwittes(user.UserId);
        }
        [Authorize]
        public async Task<UserProfileModel> GetUserProfile([CurrentUserGlobalState] CurrentUser user)
        {
            return await _twitteRepository.GetUserProfile(user.UserId);
        }
        [Authorize]
        public async Task<List<UserProfileModel>> GetAllUser([CurrentUserGlobalState] CurrentUser user)
        {
            return await _followRepository.GetAllUser(user.UserId);
        }
    }
}