using MessageLogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public User(string name, string username)
        {
            Name = name;
            Username = username;
        }

        public User LogIn(MessageLoggerContext context, User user, Query query)
        {
            Console.WriteLine("What is your username? ");
            var usernameInput = Console.ReadLine();

                var checkedUser = context.Users.Single(e => e.Username == usernameInput);
                if (checkedUser.Username == usernameInput)
                {
                    user = checkedUser;
                Console.WriteLine($"Your previous messages:\n");
                query.ConstantInfo(context, user);
                Console.WriteLine("Enter a message, or type 'log out' or 'quit': ");
                }
                else
                {
                    user = null;
                }
            return user;
        }

        public User NewUser(MessageLoggerContext context)
        {
            string name;
            string username;

            Console.WriteLine("Lets create a profile for you!\n");
            Console.Write("What is your name? ");
            name = Console.ReadLine();
            Console.Write("What is your username? (one word, no spaces!) ");
            username = Console.ReadLine();
            var user = new User(name, username);
            context.Users.Add(user);
            context.SaveChanges();
            Console.WriteLine("\nTo log out of your user profile, enter `log out`.\n");
            Console.Write("Add a message (or `quit` to exit): ");

            return user;
        }
    }
}