using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSALinqPractice
{
    public class Todo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }

        public override string ToString()
        {
            return $"\n\tTodo:\n\tId: {Id}\n\tName: {Name}\n\tIs complete?: {IsComplete}" +
                $"\n\tCreated at: {CreatedAt}\n\tUser id: {UserId}\n";
        }
    }
}