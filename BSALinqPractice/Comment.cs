using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSALinqPractice
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            return $"\n\t\tComment:\n\t\tId: {Id}\n\t\tBody: {Body}\n\t\tCreated at: {CreatedAt}" +
                $"\n\t\tPost id: {PostId}\n\t\tUser id: {UserId}\n\t\tLikes: {Likes}\n";
        }
    }
}