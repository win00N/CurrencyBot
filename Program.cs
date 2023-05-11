using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CurrencyBot;

public class Program
{
    static void Main(string[] args)
    {
        var apiKey = System.IO.File.ReadAllText("token.txt");
        var client = new TelegramBotClient(apiKey);
        client.StartReceiving(Update, Error);
        Console.ReadKey();
    }

    private async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var message = update.Message;

        if (message.Text != null)
        {
            if (message.Text.ToLower().Equals("/get"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, GetCurrencies());
            }
            if (message.Text.ToLower().Equals("/save"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "");
            }
        }

    }
    private async static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }

    private static string GetCurrencies()
    {
        var webScrapper = new WebScrapper();
        var sb = new StringBuilder();
        sb.Append("(по выходным курс валют не будет работать)\n");

        var currencies = webScrapper.GetCurrencies();

        foreach (var currency in currencies)
        {
            sb.Append($"{currency.FullName}({currency.ShortName}) - {currency.Value}(KZT)\n");
        }

        return sb.ToString();
    }
}