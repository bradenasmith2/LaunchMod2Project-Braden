using MessageLogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Models
{
    public class Query
    {
        public void Commands(string input, Query query, User user, MessageLoggerContext context)
        {
            Console.WriteLine("\nAt anytime you can type:\n\nlog out - Log out of your current profile.\ncommon words - View the most common words in this program.\nuser message count - View all user's names, along with their total message count.\nmy messages - View your lifetime messages.\n\n");

            if (input == "common words".ToLower())
            {
                query.CommonWord(context);
            }
            else if (input == "user message count".ToLower())
            {
                query.FinalInfo(context);
            }
            else if (input == "my messages".ToLower())
            {
                query.ConstantInfo(context, user);
            }
        }

        public void ConstantInfo(MessageLoggerContext context, User user)
        {
            foreach (var message in user.Messages)
            {
                Console.WriteLine($"{user.Name} {message.CreatedAt:t}: {message.Content}");
            }
        }

        public void FinalInfo(MessageLoggerContext context)
        {
            foreach (var user in context.Users.OrderByDescending(e => e.Messages.Count()))
            {
                Console.WriteLine($"{user.Name} wrote {user.Messages.Count} messages");
            }
            Console.WriteLine("Thanks for using Message Logger!");
        }

        //public void CommonWord(MessageLoggerContext context, User user)
        //{
        //    var wordList = new List<string>();
        //    var countedWords = new Dictionary<string, int>();
        //    foreach (var u in context.Users)
        //    {
        //        foreach (var m in user.Messages)
        //        {
        //            wordList.AddRange(m.Content.Split());
        //        }
        //    }
        //        foreach (var w in wordList)
        //        {
        //            if (countedWords.ContainsKey(w))
        //            {
        //                countedWords[w]++;
        //            }
        //            else if (!countedWords.ContainsKey(w))
        //            {
        //                countedWords.Add(w, 1);
        //            }
        //            else
        //            {
        //                Console.WriteLine("ERROR IN COMMONWORD()");
        //            }
        //        }
        //    var DescWords = countedWords.OrderByDescending(e => e.Value);

        //    foreach (var e in DescWords)
        //    {
        //        Console.WriteLine($"{e.Key}: {e.Value}");
        //    }
        //}

        public void CommonWord(MessageLoggerContext context)
        {
            var messageList = new List<Message>();
            var wordList = new List<string>();
            var countedWords = new Dictionary<string, int>();

            foreach (var u in context.Users)
            {
                messageList.AddRange(u.Messages);
            }
            foreach (var m in messageList)
            {
                wordList.AddRange(m.Content.ToLower().Split());
            }
            foreach (var w in wordList)
            {
                if (countedWords.ContainsKey(w))
                {
                    countedWords[w]++;
                }
                else
                {
                    countedWords.Add(w, 1);
                }
            }
            var OrderedDesc = countedWords.OrderByDescending(e => e.Value);
            int count = 0;

            foreach (var w in OrderedDesc)
            {
                if (count >= 10) break;
                Console.WriteLine($"{w.Key}: {w.Value}");
                count++;
            }
        }

        public void MessageByHour()
        {

        }
    }
}
