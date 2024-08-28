using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Astrum.Console.Bot.Service
{
    public partial class UpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient telegramBotClient,Update update,CancellationToken cancellationToken)
        {
            var task = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(telegramBotClient, update, cancellationToken),
                UpdateType.EditedMessage => HandleEditedMessageAsync(telegramBotClient, update, cancellationToken),
                UpdateType.CallbackQuery => HandleCallBackQuery(telegramBotClient, update, cancellationToken),
                _ => HandleUnknownUpdateType(telegramBotClient, update, cancellationToken)
            };
        }

        private async Task HandleUnknownUpdateType(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleCallBackQuery(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            if(update.CallbackQuery.Data == "uz")
            {
                await telegramBotClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.From.Id,
                    text: "Uzbek tili tanlandi"
                    );
            }
        }

        private async Task HandleEditedMessageAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleErrorAsync(ITelegramBotClient telegramBotClient,Exception exception,CancellationToken cancellationToken)
        {

        }
    }
}
