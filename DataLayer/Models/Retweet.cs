using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterServer.DataLayer.Models
{
    [Table("RetweetTb")]

    public class Retweet
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// twite id: whitch tiwte?
        /// </summary>
        public Guid TwitteId { get; set; }
        public Twitte Twitte { get; set; }
        /// <summary>
        /// when retwite done?
        /// </summary>
        public DateTime RetweetDatetime { set; get; }
        public Guid RetweetUserId { get; set;}
    }
}
