using MessageLogger.Data;
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
        public List<Message> Messages { get; set;  } = new List<Message>();

        public User(string name, string username)//remove constructor (after changes)
        {
            Name = name;
            Username = username;
        }

        public User LogIn(MessageLoggerContext context, User user)
        {
            Console.WriteLine("What is your username? ");
            var usernameInput = Console.ReadLine();

                var checkedUser = context.Users.Single(e => e.Username == usernameInput);
                if (checkedUser.Username == usernameInput)
                {
                    user = checkedUser;
                }
                else
                {
                    user = null;
                }
            return user;
        }

        public void LogOut()
        {

        }

        public void NewUser()
        {

        }
    }
}