using GreatTutorialBot;
using System.Threading;

namespace GreatTutorailBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bot bot = new Bot("5854978684:AAEhNYNiSXrAdbim-avmW_MtpW6CSzmKVC0");



            while (true)
            {
                Console.WriteLine("Enter Y or y To Turn On The Bot.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                {
                    bot.Start();
                }
            }
        }
    }
}