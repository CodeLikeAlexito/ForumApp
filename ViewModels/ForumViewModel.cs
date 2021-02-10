using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels
{
    public class ForumViewModel
    {
        public Topic Topic { get; set; }
        public Comment Comment { get; set; }
        public Replay Replay { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Replay> Replays { get; set; }
    }
}
