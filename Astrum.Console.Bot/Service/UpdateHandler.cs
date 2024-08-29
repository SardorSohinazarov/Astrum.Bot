using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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

            try
            {
                await task;
            }
            catch (Exception ex) 
            {
                await HandleErrorAsync(telegramBotClient,ex,cancellationToken);
            }
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
                await ContactSoraAsync(telegramBotClient, update, cancellationToken);
            }else if(update.CallbackQuery.Data == "ru")
            {
                await telegramBotClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.From.Id,
                    text: "Rustili tanlandi"
                    );
                await ContactSoraAsync(telegramBotClient, update, cancellationToken);
            }
            else if(update.CallbackQuery.Data == "en")
            {
                await telegramBotClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.From.Id,
                    text: "Ingliz tili tanlandi"
                    );
                await ContactSoraAsync(telegramBotClient, update, cancellationToken);
            }
        }


        private async Task ContactSoraAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            //oldin jo'natganmi yo'qmi

            var botton = KeyboardButton.WithRequestContact("Send My Contact");

            await telegramBotClient.SendTextMessageAsync(
                chatId: update.CallbackQuery.From.Id,
                text: "Contact",
                replyMarkup: new ReplyKeyboardMarkup(botton)
                );
        }

        private async Task HandleEditedMessageAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleErrorAsync(ITelegramBotClient telegramBotClient,Exception exception,CancellationToken cancellationToken)
        {
            await telegramBotClient.SendTextMessageAsync(
                chatId: 5617428170,
                text: $"{exception.Message} \n\n {exception.StackTrace}"
             );
        }
    }
}
