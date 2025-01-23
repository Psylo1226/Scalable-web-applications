using System;
using System.Collections.Generic;

namespace FaceSpace.Models
{
    public class FeedPost
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int Likes { get; set; }
        public List<FeedComment> Comments { get; set; } = new List<FeedComment>();
    }

    public class FeedComment
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }

    public class FeedModel
    {
        public List<FeedPost> Posts { get; set; } = new List<FeedPost>();
    }
}
