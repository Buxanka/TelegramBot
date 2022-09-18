using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.IO;
using Telegram.Bot.Types.InputFiles;

namespace TelegramBot
{
    public class Bot
    {
        public static int Rand = 0;
        static ITelegramBotClient bot = new TelegramBotClient("5454804462:AAEtI9g6blkalFPuseubc7YZxA7oN89nfQ8");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Доброго времени суток!");
                    return;
                }
                if (message.Text.ToLower() == "/hi")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Привет, милая!");
                    return;
                }
                if (message.Text.ToLower() == "/love")
                {
                    await RandomTextCats.hzhzhz();
                }
                await botClient.SendTextMessageAsync(message.Chat, "Не знаю таких слов, не понимаю :(");
            }
        }
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            return Task.CompletedTask;
        }
        public static void BotLaunch() //ReceiverOptions HandleUpdateAsync
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
    public class RandomTextCats : Bot
    {
        public static List<string> _randomTextCats = new List<string>();
        public static void PutInToListRandomTextCats()
        {
            for (int i = 0; i < 21; i++)
            {
                _randomTextCats.Add(i + ".jpg");
            }
        }
        public async Task hzhzhz (ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            PutInToListRandomTextCats();
            var message = update.Message;
            var randonLove = new Random();
            Rand = randonLove.Next(1, 20);
            for (int i = 1; i <= _randomTextCats.Count; i++)
            {
                if (Rand == i)
                {
                    using (var fileStream = new FileStream(_randomTextCats[i], FileMode.Open, FileAccess.Read, FileShare.Read))
                        await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(fileStream));
                    return;
                }
            }
        }
    }
    public class RandomText
    {

    }
    public class RandomCats
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Bot.BotLaunch();
        }
    }
}
