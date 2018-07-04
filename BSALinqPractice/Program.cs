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
            var rep = new DataRepository();
            Users = rep.GetUsers();
        }

        public static IEnumerable<(Post, int)> CountCommentsUnderPosts(int userId)
        {
            if(Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId).SelectMany(u => u.Posts)
                    .Select(p => (Post: p, CommentsAmount: p.Comments.Count()));
            }
            throw new ArgumentException("Wrong User id");
        }

        public static IEnumerable<Comment> GetShortCommentsUnderPosts(int userId)
        {
            if (Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId).SelectMany(u => u.Posts)
                    .SelectMany(p => p.Comments).Where(c => c.Body.Count() < 50);
            }
            throw new ArgumentException("Wrong User id");
        }

        public static IEnumerable<(int, string)> GetCompleteTodoes(int userId)
        {
            if (Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId).SelectMany(u => u.Todoes)
                    .Where(t => t.IsComplete == true).Select(t => (t.Id, t.Name));
            }
            throw new ArgumentException("Wrong User id");
        }

        public static IEnumerable<User> GetSortedUsers()
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

        public static (User, Post, int, int, Post, Post) GetUserInfo(int userId)
        {
            if (Users.Select(u => u.Id).Contains(userId))
            {
                return Users.Where(u => u.Id == userId)
                    .Select(u => (User: u,
                        LastPost: u.Posts.OrderByDescending(p => p.CreatedAt).First(),
                        LastPostCommentsAmount: u.Posts.OrderByDescending(p => p.CreatedAt).First().Comments.Count(),
                        UncompleteTodoesAmount: u.Todoes.Where(t => t.IsComplete == false).Count(),
                        ComentedPost: u.Posts.OrderBy(p => p.Comments.Where(c => c.Body.Count() > 80).Count()).First(),
                        LikedPost: u.Posts.OrderBy(p => p.Likes).First()
                    )).First();
            }
            throw new ArgumentException("Wrong User id");
        }

        public static (Post, Comment, Comment, int) PostInfo(int postId)
        {
            if(Users.SelectMany(u => u.Posts).Select(p => p.Id).Contains(postId))
            {
                return Users.SelectMany(u => u.Posts).Where(p => p.Id == postId)
                    .Select(p => (Post: p, 
                        LongestComment: p.Comments.OrderBy(c => c.Body.Count()).First(),
                        LikedComment: p.Comments.OrderBy(c => c.Likes).First(),
                        TrashCommentsAmount: p.Comments.Where(c => c.Likes == 0 || c.Body.Count() < 80).Count()
                    )).First();
            }
            throw new ArgumentException("Wrong User id");
        }

        
    }
}
