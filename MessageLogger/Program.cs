using MessageLogger.Data;
using MessageLogger.Models;
using Microsoft.EntityFrameworkCore;

    Console.WriteLine("Welcome to Message Logger!\n");
    Console.WriteLine("Do you already have an account with us? Please type either 'y' or 'n'");
var response = Console.ReadLine();

User user = new User(null, null);
using (var context = new MessageLoggerContext())
{
    if (response == "y".ToLower() || response == "yes".ToLower())
    {
        user = user.LogIn(context, user);
        context.SaveChanges();
    }
    else if (response == "n".ToLower() || response == "no".ToLower())
    {
        user = user.NewUser(context);
        context.SaveChanges();
    }
}

    string userInput = Console.ReadLine();
    List<User> users = new List<User>() { user };

using (var context = new MessageLoggerContext())
{
    var DbUser = context.Users.Find(user.Id);
    DbUser.Messages.Add(new Message(userInput));
    context.SaveChanges();
}
    while (userInput.ToLower() != "quit")
    {
        while (userInput.ToLower() != "log out")
        {
        using (var context = new MessageLoggerContext())//prints immediate info (Methodize)
        {
            var DbUser = context.Users.Find(user.Id);
            foreach (var message in DbUser.Messages)
            {
                Console.WriteLine($"{user.Name} {message.CreatedAt:t}: {message.Content}");
            }
        }

        Console.Write("Add a message: ");
        
        userInput = Console.ReadLine();

        using (var context = new MessageLoggerContext())//ensures no "quit" or "log out" message is added (Methodize)
        {
            if (userInput != "quit" && userInput != "log out")
            {
                var DbUser = context.Users.Find(user.Id);
                user.Messages.Add(new Message(userInput));
                DbUser.Messages.Add(new Message(userInput));
                context.SaveChanges();
            }
        }
        }

        Console.Write("\nWould you like to log in a `new` or `existing` user? Or, `quit`? ");
        userInput = Console.ReadLine();
        if (userInput.ToLower() == "new")
        {
            using (var context = new MessageLoggerContext())
            {
                user = user.NewUser(context);
                userInput = Console.ReadLine();
                var DbUser = context.Users.Find(user.Id);
                DbUser.Messages.Add(new Message(userInput));
                context.SaveChanges();
            }
        }

        else if (userInput.ToLower() == "existing")
        {
            using (var context = new MessageLoggerContext())
            {
                user = user.LogIn(context, user);
                context.SaveChanges();
            }
                if (user != null)
                {
                    using (var context = new MessageLoggerContext())
                    {
                        var DbUser = context.Users.Find(user.Id);
                        DbUser.Messages.Add(new Message(userInput));
                        context.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("could not find user");
                    userInput = "quit";
                }
        }
    }


Console.WriteLine("Thanks for using Message Logger!");//final information display (Methodize+OrderBy)
using (var context = new MessageLoggerContext())
{
    foreach (var u in context.Users)
    {
        Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");
    }
}

//TO DO:

//Console.Clear's
//Methods for: FinalInfo(), ConstantInfo(), CommandCheck()
    //move if(user != null){} into NewUser()?

//Third Iteration Methods: FinalInfo(), CommonWord(User? user), MessageByHour() [New 'Query' Class?]

    //TESTS FOR ALL METHODS!
