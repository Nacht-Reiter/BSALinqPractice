using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSALinqPractice
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public override string ToString()
        {
            string res = $"\tPost:\n\tId: {Id}\n\tTitle: {Title}\n\tBody: {Body}" +
                $"\n\tCreated at: {CreatedAt}\n\tUser id: {UserId}\n\tLikes: {Likes}\n\tComments: \n";
            foreach(var i in Comments)
            {
                res += i.ToString();
            }
            return res;
        }
    }
}