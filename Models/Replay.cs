using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Replay
    {
        [Key]
        [Column("replay_id")]
        public int ReplayId { get; set; }
        [Column("replay_description")]
        public string ReplayDescription { get; set; }
        [Column("comment_id")]
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}
