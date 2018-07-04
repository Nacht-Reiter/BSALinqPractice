using System;
using System.Collections.Generic;
using System.Linq;

namespace BSALinqPractice
{
    static class Program
    {
        public static IEnumerable<User> Users { get; private set; }

        static void Main()
        {
            Console.WriteLine("Loading . . .");
            var rep = new DataSource();
            Users = rep.GetUsers();
            Menu.MainMenu();
        }

        public static IEnumerable<(Post, int)> CountCommentsUnderPosts(int userId)//1 required query
        {
            if(Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId).SelectMany(u => u.Posts)
                    .Select(p => (Post: p, CommentsAmount: p.Comments.Count()));
            }
            throw new ArgumentException("Wrong User id");
        }

        public static IEnumerable<Comment> GetShortCommentsUnderPosts(int userId)//2 required query
        {
            if (Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId).SelectMany(u => u.Posts)
                    .SelectMany(p => p.Comments).Where(c => c.Body.Count() < 50);
            }
            throw new ArgumentException("Wrong User id");
        }

        public static IEnumerable<(int, string)> GetCompleteTodoes(int userId)//3 required query
        {
            if (Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId).SelectMany(u => u.Todoes)
                    .Where(t => t.IsComplete == true).Select(t => (t.Id, t.Name));
            }
            throw new ArgumentException("Wrong User id");
        }

        public static IEnumerable<User> GetSortedUsers()//4 required query
        {
            return Users.OrderBy(u => u.Name).Select(
                u => new User
                {
                    Todoes = u.Todoes.OrderByDescending(t => t.Name.Count()),
                    Posts = u.Posts,
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    Avatar = u.Avatar,
                    Email = u.Email
                });
        }

        public static (User, Post, int, int, Post, Post) GetUserInfo(int userId)//5 required query
        {
            if (Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId)
                    .Select(u => (User: u,
                        LastPost: u.Posts.OrderByDescending(p => p.CreatedAt).First(),
                        LastPostCommentsAmount: u.Posts.OrderByDescending(p => p.CreatedAt).First().Comments.Count(),
                        UncompleteTodoesAmount: u.Todoes.Where(t => t.IsComplete == false).Count(),
                        ComentedPost: u.Posts.OrderByDescending(p => p.Comments.Where(c => c.Body.Count() > 80).Count()).FirstOrDefault(),
                        LikedPost: u.Posts.OrderByDescending(p => p.Likes).FirstOrDefault()
                    )).First();
            }
            throw new ArgumentException("Wrong User id");
        }

        public static (Post, Comment, Comment, int) GetPostInfo(int postId)//6 required query
        {
            if(Users.SelectMany(u => u.Posts).Select(p => p.Id).Contains(postId))
            {
                return Users.SelectMany(u => u.Posts).Where(p => p.Id == postId)
                    .Select(p => (Post: p,
                        LongestComment: p.Comments.OrderByDescending(c => c.Body.Count()).FirstOrDefault(),
                        LikedComment: p.Comments.OrderByDescending(c => c.Likes).FirstOrDefault(),
                        TrashCommentsAmount: p.Comments.Where(c => c.Likes == 0 || c.Body.Count() < 80).Count()
                    )).First();
            }
            throw new ArgumentException("Wrong User id");
        }
    }
}
