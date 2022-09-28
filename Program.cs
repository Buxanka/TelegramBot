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
        public string sdf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static int Rand = 0;
        public static string PATHTEXTCAT = Environment.CurrentDirectory + @"\RandomTextCats\";
        public static string PATHKIT = Environment.CurrentDirectory + @"\RandomKit\";
        static ITelegramBotClient bot = new TelegramBotClient("5454804462:AAEtI9g6blkalFPuseubc7YZxA7oN89nfQ8");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Доброго времени суток!");
                    return;
                }
                if (message.Text.ToLower() == "/support")
                {
                    RandomText.File();
                    var randomText = new Random();
                    Rand = randomText.Next(0, 17);
                    for (int i = 1; i < RandomText._randomText.Count; i++)
                    {
                        if (Rand == i)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, RandomText._randomText[i]);
                        }
                    }
                    return;
                }
                if (message.Text.ToLower() == "/love")
                {
                    RandomTextCats.PutInToListRandomTextCats();
                    var randomLove = new Random();
                    Rand = randomLove.Next(0, 49);
                    for (int i = 1; i <= RandomTextCats._randomTextCats.Count; i++)
                    {
                        if (Rand == i)
                        {
                            using (var fileStream = new FileStream(PATHTEXTCAT + RandomTextCats._randomTextCats[i], FileMode.Open, FileAccess.Read, FileShare.Read))
                                await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(fileStream));
                            return;
                        }
                    }
                }
                if (message.Text.ToLower() == "/kit")
                {
                    RandomKit.PutInToListRandomKit();
                    var randomKit = new Random();
                    Rand = randomKit.Next(0, 25);
                    for (int i = 1; i <= RandomKit._randomKit.Count; i++)
                    {
                        if (Rand == i)
                        {
                            using (var fileStream = new FileStream(PATHKIT + RandomKit._randomKit[i], FileMode.Open, FileAccess.Read, FileShare.Read))
                                await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(fileStream));
                            return;
                        }
                    }
                }
                await botClient.SendTextMessageAsync(message.Chat, "Не знаю таких слов, не понимаю :(");
            }
        }
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            return Task.CompletedTask;
        }
        public static void BotLaunch()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
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
    public class RandomTextCats
    {
        public static List<string> _randomTextCats = new List<string>();
        public static void PutInToListRandomTextCats()
        {
            for (int i = 0; i < 51; i++)
            {
                _randomTextCats.Add(i + ".jpg");
            }
        }
    }
    public class RandomText
    {
        public static List<string> _randomText = new List<string>();
        public static string path = "RandomText.txt";
        public async static void File()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string text;
                while ((text = await reader.ReadLineAsync()) != null)
                {
                    _randomText.Add(text);
                }
            }

        }
    }
    public class RandomKit
    {
        public static List<string> _randomKit = new List<string>();
        public static void PutInToListRandomKit()
        {
            for (int i = 0; i < 51; i++)
            {
                _randomKit.Add(i + ".jpg");
            }
        }
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
