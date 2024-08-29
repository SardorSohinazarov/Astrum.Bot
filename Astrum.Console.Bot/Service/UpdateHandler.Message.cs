using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Astrum.Console.Bot.Service
{
    public partial class UpdateHandler
    {
        private async Task HandleMessageAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;

            var sentMessage = message.Type switch
            {
                MessageType.Text => HandleTextAsync(telegramBotClient, message, cancellationToken),
                MessageType.Video => HandleVideoAsync(telegramBotClient, message, cancellationToken),
                MessageType.Photo => HandlePhotoAsync(telegramBotClient, message, cancellationToken),
                MessageType.Location => HandleLocationAsync(telegramBotClient, message, cancellationToken),
                MessageType.Contact => HandleContactAsync(telegramBotClient, message, cancellationToken),
                MessageType.Voice => HandleVoiceAsync(telegramBotClient, message, cancellationToken),
                _ => HandleUnknownMessageTypeAsync(telegramBotClient, message, cancellationToken)
            };
        }

        private async Task HandleUnknownMessageTypeAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleVoiceAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleContactAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            // contactni saqla
        }

        private async Task HandleLocationAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            var location = message.Location;

            await telegramBotClient.SendLocationAsync(
                chatId: message.Chat.Id,
                latitude: location.Latitude,
                longitude: location.Longitude
                );
        }

        private async Task HandlePhotoAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleVideoAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
