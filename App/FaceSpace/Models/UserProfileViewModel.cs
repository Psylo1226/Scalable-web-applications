using System;
using System.Collections.Generic;

namespace FaceSpace.Models
{
    // Model dla komentarzy
    public class Comment
    {
        public string Author { get; set; } // Autor komentarza
        public string Text { get; set; } // Treść komentarza
    }

    // Model dla postów
    public class Post
    {
        public string Author { get; set; } // Autor posta
        public string Content { get; set; } // Treść posta
        public DateTime Timestamp { get; set; } // Czas publikacji posta
        public int Likes { get; set; } // Liczba polubień
        public List<Comment> Comments { get; set; } = new List<Comment>(); // Lista komentarzy
    }

    // Model profilu użytkownika
    public class UserProfileViewModel
    {
        public string Name { get; set; } = "John Doe"; // Imię i nazwisko użytkownika
        public string Description { get; set; } = "Welcome to your profile!"; // Opis profilu
        public string SelectedAvatar { get; set; } = "/img/avatar1.png"; // Wybrany awatar
        public List<string> AvatarOptions { get; set; } = new List<string> // Dostępne awatary
        {
            "/img/avatar1.png",
            "/img/avatar2.png",
            "/img/avatar3.png",
            "/img/avatar4.png"
        };

        // Lista znajomych
        public List<Friend> Friends { get; set; } = new List<Friend>
        {
            new Friend { Name = "James Pittman", Avatar = "/img/avatar2.png" },
            new Friend { Name = "Ella Cabena", Avatar = "/img/avatar3.png" }
        };

        // Lista sugerowanych znajomych
        public List<Friend> SuggestedFriends { get; set; } = new List<Friend>
        {
            new Friend { Name = "Mitchell Ashcroft", Avatar = "/img/avatar4.png" },
            new Friend { Name = "Laura Pollock", Avatar = "/img/avatar2.png" }
        };

        // Lista postów na timeline
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

    // Model dla znajomych
    public class Friend
    {
        public string Name { get; set; } // Imię i nazwisko znajomego
        public string Avatar { get; set; } // Awatar znajomego
    }
}
