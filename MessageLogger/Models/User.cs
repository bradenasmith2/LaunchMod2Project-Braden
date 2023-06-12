using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public List<Message> Messages { get; } = new List<Message>();

        public User(string name, string username)//remove constructor (after changes)
        {
            Name = name;
            Username = username;
        }
    }
}
