// See https://aka.ms/new-console-template for more information
using MessageLogger.Models;

Console.WriteLine("Welcome to Message Logger!");
Console.WriteLine();
Console.WriteLine("Let's create a user pofile for you.");
Console.Write("What is your name? ");
string name = Console.ReadLine();
Console.Write("What is your username? (one word, no spaces!) ");
string username = Console.ReadLine();
User user = new User(name, username); //initial creation of a User using UserInputs

Console.WriteLine();
Console.WriteLine("To log out of your user profile, enter `log out`.");

Console.WriteLine();
Console.Write("Add a message (or `quit` to exit): ");

string userInput = Console.ReadLine();//this is the input  for the initial Message.
List<User> users = new List<User>() { user };//adding the initial user to a list of Users

while (userInput.ToLower() != "quit")//this loop keeps the program running until it is told to quit
{
    while (userInput.ToLower() != "log out")//this loop keeps the program running with a specific user until it is told to logout, but log out doesn't end the program
                                            //since this loop is nested in the 'quit' loop.
    {
        user.Messages.Add(new Message(userInput));//initial message being saved THROUGH User

        foreach (var message in user.Messages)
        {
            Console.WriteLine($"{user.Name} {message.CreatedAt:t}: {message.Content}");//prints basic information after each input.
        }

        Console.Write("Add a message: ");

        userInput = Console.ReadLine();
        Console.WriteLine();//Continues looping to add Messages until it is told to 'log out'
    }

    Console.Write("Would you like to log in a `new` or `existing` user? Or, `quit`? ");
    userInput = Console.ReadLine();
    if (userInput.ToLower() == "new")
    {
        Console.Write("What is your name? ");
        name = Console.ReadLine();
        Console.Write("What is your username? (one word, no spaces!) ");
        username = Console.ReadLine();
        user = new User(name, username);//creating a new user
        users.Add(user);//adding new user to the list of users that was initialized for the first User created (line 20)
        Console.Write("Add a message: ");

        userInput = Console.ReadLine();//collects a message then loops back to the while loops to repeat all functionality.

    }
    else if (userInput.ToLower() == "existing")
    {
        Console.Write("What is your username? ");
        username = Console.ReadLine();
        user = null;//forcing user to be null to ensure the next few steps function properly.
        foreach (var existingUser in users)
        {
            if (existingUser.Username == username)
            {
                user = existingUser;//setting the 'exisitingUser' to 'user', which will then retrieve information from that User,
                //if this line wasn't here, the 'existingUser' would be setup as a new user, and their information would not be available.
            }
        }
        
        if (user != null)
        {
            Console.Write("Add a message: ");//adding messages if that existingUser WAS an existing user.
            userInput = Console.ReadLine();
        }
        else
        {
            Console.WriteLine("could not find user");//forcing the program to end if the user inputs a nonexistant username.
            userInput = "quit";

        }
    }

}

Console.WriteLine("Thanks for using Message Logger!");
foreach (var u in users)
{
    Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");//prints information from ALL users with a count of ALL messages for EACH user.
}

//SEE Data > programChanges.txt to view the changes that will be made
