using System;
using System.Collections.Generic;

namespace FaceSpace.Models
{
    public class Comment
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }

    public class Post
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class UserProfileViewModel
    {
        public string Name { get; set; } = "John Doe";
        public string Description { get; set; } = "Welcome to your profile!";
        public string SelectedAvatar { get; set; } = "/img/avatar1.png";
        public List<string> AvatarOptions { get; set; } = new List<string>
        {
            "/img/avatar1.png",
            "/img/avatar2.png",
            "/img/avatar3.png",
            "/img/avatar4.png"
        };

        // Nowe pole dla postów na timeline
        public List<Post> TimelinePosts { get; set; } = new List<Post>
        {
            new Post
            {
                Author = "John Doe",
                Content = "This is a sample post content. It can contain any text or links.",
                Timestamp = DateTime.Now.AddMinutes(-5),
                Likes = 10,
                Comments = new List<Comment>
                {
                    new Comment { Author = "Jane Smith", Text = "This is a great post!" },
                    new Comment { Author = "Mark Johnson", Text = "Totally agree with you!" }
                }
            },
            new Post
            {
                Author = "John Doe",
                Content = "Here's another post to fill the timeline!",
                Timestamp = DateTime.Now.AddHours(-2),
                Likes = 5,
                Comments = new List<Comment>
                {
                    new Comment { Author = "Alice Brown", Text = "Love this!" }
                }
            }
        };
    }
}
