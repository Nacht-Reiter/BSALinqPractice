using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSALinqPractice
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }

        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Todo> Todoes { get; set; }

        public override string ToString()
        {
            string posts = "";
            string todoes = "";
            foreach (var i in Posts)
            {
                posts += i.ToString();
            }
            foreach (var i in Todoes)
            {
                todoes += i.ToString();
            }
            return $"\nUser:\nId: {Id}\nName: {Name}\nAvatar: {Avatar}" +
                $"\nCreated at: {CreatedAt}\nEmail: {Email}\nPosts:\n{posts}Todoes: \n{todoes}";
        }
    }   
}