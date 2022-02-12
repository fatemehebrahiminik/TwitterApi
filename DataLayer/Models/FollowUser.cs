using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace TwitterServer.DataLayer.Models
{
    [Table("FollowUserTb")]
    public class FollowUser
    {
       
        [Key]
        public int Id { get; set; } 
        public Guid TwitterUserId { get; set; }
        public TwitterUser TwitterUser { get; set; }
        public Guid FollowerUserId { get; set; }
        public TwitterUser FollowerUser { get; set; }
    }
}
