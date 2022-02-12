using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterServer.DataLayer.Models
{
    [Table("TwitterUserTb")]
    public class TwitterUser
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { set; get; }
        public ICollection<Twitte> Twittes { get; set; } 
        public ICollection<FollowUser> Followers { get; set; }
        public ICollection<FollowUser> Following { get; set; }
    }
}
