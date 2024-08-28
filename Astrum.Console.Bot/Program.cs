using Astrum.Console.Bot.Service;
using Telegram.Bot;
using Telegram.Bot.Types;

internal class Program
{
    private static void Main(string[] args)
    {
        var botclient = new TelegramBotClient("{token}");

        var updateHandler = new UpdateHandler();

        botclient.StartReceiving(updateHandler:  updateHandler.HandleUpdateAsync,
                                 pollingErrorHandler: updateHandler.HandleErrorAsync,
                                 receiverOptions: new Telegram.Bot.Polling.ReceiverOptions(){  ThrowPendingUpdates = true },
                                 cancellationToken: default
                                 );

        Console.ReadLine();
    }
}