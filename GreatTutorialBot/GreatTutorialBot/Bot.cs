using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace GreatTutorialBot
{
    public class Bot
    {
        private readonly TelegramBotClient _botClient;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        public Bot(string token)
        {
            _botClient = new TelegramBotClient(token);
        }

        public void Start()
        {
            _botClient.StartReceiving(
                updateHandler: handleUpdateAsync,
                cancellationToken: _cancellationTokenSource.Token,
                pollingErrorHandler: pollingErrorHandler);
            Console.WriteLine("\n \n Bot Started \n \n");


            Console.WriteLine("Enter N or n To Shut Down The Bot.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == 'N' || key.KeyChar == 'n')
                {
                    _cancellationTokenSource.Cancel();
                    Console.WriteLine("\n \n Bot stopped! \n \n ");
                    break;
                }
            }
        }


        private async Task handleUpdateAsync(ITelegramBotClient arg1, Update arg2, CancellationToken arg3)
        {
            if(arg2.Message?.Text is { } messageText)
            {
                var chatID = arg2.Message.Chat.Id;

                if(messageText.ToLower() == "good morning")
                {
                    await _botClient.SendTextMessageAsync(chatId: chatID
                        , text: $"Good Evening",
                        cancellationToken: _cancellationTokenSource.Token
                        );
                }
                else
                {
                    await _botClient.SendTextMessageAsync(chatId: chatID
                          , text: $"Hey",
                          cancellationToken: _cancellationTokenSource.Token
                          );
                }
                Console.WriteLine($"Received a '{messageText}' message in chat {chatID}.");
            }
        }


        private Task pollingErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            var errorMessage = (arg2 is ApiRequestException apiRequestException)
                ? $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}"
                : arg2.ToString();

            Console.WriteLine(errorMessage);

            return Task.CompletedTask;
        }
    }
}
