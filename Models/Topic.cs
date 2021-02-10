using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Topic
    {
        [Key]
        [Column("topic_id")]
        public int TopicId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("topic_description")]
        public string Description { get; set; }
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public List<Comment> Comments { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
    }
}
