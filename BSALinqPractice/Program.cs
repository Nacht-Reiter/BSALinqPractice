using System;
using System.Collections.Generic;

namespace BSALinqPractice
{
    class Program
    {
        static void Main()
        {
            var rep = new DataRepository();
            IEnumerable<User> users = rep.GetUsers();
        }
    }
}
