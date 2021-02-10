using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Comment
    {
        [Key]
        [Column("comment_id")]
        public int CommentId { get; set; }
        [Column("text")]
        public string Text { get; set; }
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Column("topic_id")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public List<Replay> Replies { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }
        [NotMapped]
        public string UserName { get; set; }

    }
}
