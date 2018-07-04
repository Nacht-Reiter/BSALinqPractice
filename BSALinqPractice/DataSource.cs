using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BSALinqPractice
{
    public class DataSource
    {
        private string RowUsers { get; set; }
        private string RowComments { get; set; }
        private string RowPosts { get; set; }
        private string RowTodoes { get; set; }
        private string RowAddresses { get; set; }

        public DataSource()
        {
            RowUsers = GetRowData("users").GetAwaiter().GetResult();
            RowPosts = GetRowData("posts").GetAwaiter().GetResult();
            RowComments = GetRowData("comments").GetAwaiter().GetResult();
            RowTodoes = GetRowData("todos").GetAwaiter().GetResult();
            RowAddresses = GetRowData("address").GetAwaiter().GetResult();
        }

        private async Task<string> GetRowData(string route) //gets json as string from api
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://5b128555d50a5c0014ef1204.mockapi.io/{route}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            client.Dispose();
            return responseBody;
        }

        #region Deserializing
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(RowUsers);
            users = users.GroupJoin(GetPosts(), u => u.Id, p => p.UserId,
            (u, p) => new User
            {
                Posts = p,
                Id = u.Id,
                Name = u.Name,
                CreatedAt = u.CreatedAt,
                Avatar = u.Avatar,
                Email = u.Email
            });

            return users.GroupJoin(GetTodoes(), u => u.Id, t => t.UserId,
            (u, t) => new User
            {
                Todoes = t,
                Posts = u.Posts,
                Id = u.Id,
                Name = u.Name,
                CreatedAt = u.CreatedAt,
                Avatar = u.Avatar,
                Email = u.Email
            });
        }

        private IEnumerable<Post> GetPosts()
        {
            IEnumerable<Post> posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(RowPosts);
            return posts.GroupJoin(GetComments(), p => p.Id, c => c.PostId,
            (pt, cm) => new Post
            {
                Comments = cm,
                Id = pt.Id,
                Body = pt.Body,
                CreatedAt = pt.CreatedAt,
                Title = pt.Title,
                UserId = pt.UserId,
                Likes = pt.Likes
            });
        }

        private IEnumerable<Comment> GetComments()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Comment>>(RowComments);
        }

        private IEnumerable<Todo> GetTodoes()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Todo>>(RowTodoes);
        }

        public IEnumerable<Address> GetAddresses()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Address>>(RowAddresses);
        }
        #endregion Deserializing

    }
}