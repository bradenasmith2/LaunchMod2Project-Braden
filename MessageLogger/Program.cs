using MessageLogger.Data;
using MessageLogger.Models;

    Console.WriteLine("Welcome to Message Logger!\n");
    Console.WriteLine("Let's create a user pofile for you.");
    Console.Write("What is your name? ");
    string name = Console.ReadLine();

    Console.Write("What is your username? (one word, no spaces!) ");
    string username = Console.ReadLine();

    User user = new User(name, username);

    Console.WriteLine("\nTo log out of your user profile, enter `log out`.\n");
    Console.Write("Add a message (or `quit` to exit): ");
    string userInput = Console.ReadLine();

List<User> users = new List<User>() { user };

using (var context = new MessageLoggerContext())
{
    
    context.Users.Add(user);
    context.SaveChanges();

    var DbUser = context.Users.Find(user.Id);
    DbUser.Messages.Add(new Message(userInput));
    user.Messages.Add(new Message(userInput));
    context.SaveChanges();
}
    while (userInput.ToLower() != "quit")
    {
        while (userInput.ToLower() != "log out")
        {
        foreach (var message in user.Messages)
        {
            Console.WriteLine($"{user.Name} {message.CreatedAt:t}: {message.Content}");
        }
        //using (var context = new MessageLoggerContext())                              //"Index is out of range" on u.Messages[0]
        //{

        //    var i = 0;
        //    foreach (var u in context.Users)
        //    {
        //        Console.WriteLine($"{u.Name} at {u.Messages[0].CreatedAt:t}: {u.Messages[0].Content}");
        //        i++;
        //    }
        //}

            Console.Write("Add a message: ");
        
            userInput = Console.ReadLine();

        using (var context = new MessageLoggerContext())//.find on context table (PK)
        {
            if (userInput != "quit" && userInput != "log out")//prevents these strings from being added to (OR counted) messages
            {
                var DbUser = context.Users.Find(user.Id);
                user.Messages.Add(new Message(userInput));//this line only adds messages (no Id, no D.T) but this allows information to print at the end.
                DbUser.Messages.Add(new Message(userInput));//replace c#'user' with above Db 'User'
                context.SaveChanges();
            }
        }
            Console.WriteLine();
        }

        Console.Write("Would you like to log in a `new` or `existing` user? Or, `quit`? ");
        userInput = Console.ReadLine();
        if (userInput.ToLower() == "new")
        {
            Console.Write("What is your name? ");
            name = Console.ReadLine();
            Console.Write("What is your username? (one word, no spaces!) ");
            username = Console.ReadLine();
            user = new User(name, username);
            users.Add(user);
            Console.Write("Add a message: ");

            userInput = Console.ReadLine();

        }
        else if (userInput.ToLower() == "existing")
        {
        using (var context = new MessageLoggerContext())
        {
            user.LogIn(context, user);
        }
            //Console.Write("What is your username? ");
            //username = Console.ReadLine();
            //user = null;
            //foreach (var existingUser in users)
            //{
            //    if (existingUser.Username == username)
            //    {
            //        user = existingUser;
            //    }
            //}

            if (user != null)
            {
                Console.Write("Add a message: ");
                userInput = Console.ReadLine();
            using (var context = new MessageLoggerContext())
            {
                var DbUser = context.Users.Find(user.Id);
                DbUser.Messages.Add(new Message (userInput));
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
Console.WriteLine("Thanks for using Message Logger!");
//foreach (var u in users)
//{
//        Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");
//}

using (var context = new MessageLoggerContext())
{
    foreach (var u in context.Users)
    {
        Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");
    }
}

//SEE Data > programChanges.txt to view the changes that will be made

//'log out' is being tracked in the Db. Need a .Remove from <Message>
