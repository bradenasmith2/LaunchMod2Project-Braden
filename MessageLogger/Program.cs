using MessageLogger.Data;
using MessageLogger.Models;
using Microsoft.EntityFrameworkCore;

    Console.WriteLine("Welcome to Message Logger!\nIf you would like to see a list of commands please type 'commands'");
    Console.WriteLine("Do you already have an account with us? Please type either 'y' or 'n'");
var response = Console.ReadLine();

Query query = new Query();
User user = new User(null, null);
using (var context = new MessageLoggerContext())
{
    if (response == "y".ToLower() || response == "yes".ToLower())
    {
        user = user.LogIn(context, user, query);
        context.SaveChanges();
    }
    else if (response == "n".ToLower() || response == "no".ToLower())
    {
        user = user.NewUser(context);
        context.SaveChanges();
    }
    else if (response == "c".ToLower() || response == "commands".ToLower() || response == "command")
    {
        query.Commands(response, query, user, context);
    }
}

    string userInput = Console.ReadLine();
    List<User> users = new List<User>() { user };

using (var context = new MessageLoggerContext())
{
    var DbUser = context.Users.Find(user.Id);
    if (userInput != "quit".ToLower() && userInput != "log out".ToLower() && userInput != "c".ToLower() && userInput != "command".ToLower() && userInput != "commands".ToLower())
    {
        DbUser.Messages.Add(new Message(userInput));
        context.SaveChanges();
        query.ConstantInfo(context, DbUser);//NOT CAUSING THE BUG!
    }
}
    while (userInput.ToLower() != "quit")
    {
        while (userInput.ToLower() != "log out")
        {
        using (var context = new MessageLoggerContext())
        {
            var DbUser = context.Users.Find(user.Id);
            var ListOfUsernames = new List<string>();
            ListOfUsernames = DbUser.UsernameList(context);

            //var singleUser = query.CommonWord(context, DbUser, userInput);
            if (userInput == "commands".ToLower() || userInput == "common words" || userInput == "my messages" || userInput == "user message count")
            {
                query.Commands(userInput, query, DbUser, context);

            }
            else
            {
                query.ConstantInfo(context, DbUser);
            }
        }

        Console.Write("Add a message: ");
        userInput = Console.ReadLine();

        using (var context = new MessageLoggerContext())
        {
            if (userInput != "quit".ToLower() && userInput != "log out".ToLower() && userInput != "c".ToLower() && userInput != "command".ToLower() && userInput != "commands".ToLower() && userInput != "common words".ToLower() && userInput != "my messages".ToLower() && userInput != "user message count".ToLower())
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
                user = user.LogIn(context, user, query);
                context.SaveChanges();
            }
                if (user != null)
                {
                    using (var context = new MessageLoggerContext())
                    {
                        var DbUser = context.Users.Find(user.Id);
                        if (userInput != "existing")
                        {
                            DbUser.Messages.Add(new Message(userInput));
                            context.SaveChanges();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("could not find user");
                    userInput = "quit";
                }
        }
    }

using (var context = new MessageLoggerContext())
{
    query.FinalInfo(context);
}

//TO DO:

//Console.Clear's

//move if(user != null){} into NewUser()?

//Third Iteration Methods: MessageByHour()

    //TESTS FOR ALL METHODS!


//known bugs: if you type "commands" before selecting 'y' or 'n' for having a profile, the program ends.
    //if you type "commands" as the first message immediately after signing in to an existing account, nothing happens, you are asked again for an input.
    //common words command runs twice.
    //upon logging in to an already created profile, your
