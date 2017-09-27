using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class MessageReply : BaseEntity
    {
        public int MessageReplyId { get; set; }
        public int MessageId { get; set; }
        public string ReplyContent { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
