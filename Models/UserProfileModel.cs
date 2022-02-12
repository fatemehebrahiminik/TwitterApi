using System;
using System.Collections.Generic;

namespace TwitterServer.Models
{
    public class UserProfileModel
    {
        public Guid Id { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public int FollowerCount { set; get; }
        public int FollowingCount { set; get; }
        public List<TwitteModel> Twittes { set; get; }
        public List<TwitteModel> ReTwittes { set; get; }

    }
}
