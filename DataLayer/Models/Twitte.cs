
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterServer.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Table("TwitteTb")]
    public class Twitte
    {
        [Key]
        public Guid Id { get; set; } 
        public string TwitteText { get; set; }
        public DateTime CreateDatetime { set; get; } 
        public Guid TwitterUserId { get; set; }
        public TwitterUser TwitterUser { get; set; } 
    }
}
