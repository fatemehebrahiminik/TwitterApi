using HotChocolate.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Twitter.Graph.Inputs;
using Twitter.GraphQL.Twitte;
using TwitterServer.Auth;
using TwitterServer.DataLayer.Models;
using TwitterServer.Graph.Outputs;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.GraphQL
{
    public class Mutation
    {
        private readonly ITwitterUserRepository _twitterUserRepository;
        private readonly IFollowRepository _followRepository;
        private readonly ITwitteRepository _twitteRepository;
        public Mutation(ITwitterUserRepository twitterUserRepository, IFollowRepository followRepository, ITwitteRepository twitteRepository)
        {
            _twitterUserRepository = twitterUserRepository;
            _followRepository = followRepository;
            _twitteRepository = twitteRepository;
        }
        public async Task<LoginResultOutput> UserLogin(LoginInput input)
        {
            return await _twitterUserRepository.LoginTwitterUser(input);
        }
        public async Task<bool> RegisterUserAsync(RegisterUserInput input)
        {
            var user = new TwitterUser
            {
                Password = input.password,//for secure use hash password
                UserName = input.username,
                Email = input.email,
                FullName = input.fullName,
            };
            return await _twitterUserRepository.RegisterTwitterUser(user); 
        }


        [Authorize]
        public async Task<TwitteResultOutput> CreateTwitte([CurrentUserGlobalState] CurrentUser user, string twitteText)
        {
            return await _twitteRepository.CreateTwitte(new Graph.Inputs.CreateTwitteInput(user.UserId, twitteText));
        }
        [Authorize]
        public async Task<TwitteResultOutput> CreateReTwitte([CurrentUserGlobalState] CurrentUser user, string twitteId)
        {
            return await _twitteRepository.CreateReTwitte(new Graph.Inputs.CreateReTwitteInput(Guid.Parse(twitteId) , user.UserId));
        }
        [Authorize]
        public async Task<bool> AddFollow([CurrentUserGlobalState] CurrentUser user, string followUserId)
        {
            return await _followRepository.AddFollow(user.UserId, Guid.Parse(followUserId));
        }
    }
}