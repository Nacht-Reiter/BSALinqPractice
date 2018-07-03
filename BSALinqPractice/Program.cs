using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BSALinqPractice
{
    class Program
    {
        static void Main()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/comments").GetAwaiter().GetResult(); ;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = responseContent.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                Console.Write(json);
            }
            Console.Read();
        }
    }
}
