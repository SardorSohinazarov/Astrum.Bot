using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Astrum.Console.Bot.Models;
using Telegram.Bot.Types.Enums;
using Astrum.Console.Bot.Repositories.Courses;

namespace Astrum.Console.Bot.Service
{
    public partial class UpdateHandler
    {
        private async Task HandleTextAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            var command = message.Text switch
            {
                "/start" => HandleStartCommandAsync(telegramBotClient,message, cancellationToken),
                "/courses" => HandleCoursesCommandAsync(telegramBotClient,message, cancellationToken),
                "/teachers" => HandleTeachersCommandAsync(telegramBotClient,message, cancellationToken),
                "/faq" => HandleFaqCommandAsync(telegramBotClient,message, cancellationToken),
                "/support" => HandleSupportCommandAsync(telegramBotClient,message, cancellationToken),
                "/contact" => HandleContactCommandAsync(telegramBotClient,message, cancellationToken),
                _ => HandleUnknownCommandAsync(telegramBotClient,message, cancellationToken),
            };

            await command;
        }

        private async Task HandleUnknownCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            var groupId = -1002191976238;

            if(message.ReplyToMessage is null) 
            {
                await telegramBotClient.ForwardMessageAsync(
                    chatId: groupId,
                    fromChatId: message.Chat.Id,
                    messageId: message.MessageId);
            }
            else{

                var chatId = message.ReplyToMessage.ForwardFrom.Id;

                await telegramBotClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"<b>Astrum jamosi</b>\n{message.Text}",
                    parseMode: ParseMode.Html
                    );
            }
        }

        private async Task HandleContactCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleSupportCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            
        }

        private async Task HandleFaqCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            var faqs = new List<Faq>()
            {
                new Faq(){
                    Id = 1,
                    Question = "Qales?",
                    Answer = "Rahmat yaxshi"
                }
            };

            StringBuilder textFaq = new StringBuilder();
            
            for(var i = 0; i < faqs.Count; i ++) {
                textFaq.Append($"\n{i+1} \n<b>Savol</b>:{faqs[i].Question}\n <b>Javob</b>:{faqs[i].Answer}");
            }

            textFaq.AppendLine("\n\nAgar bu joyda sizning muammoingiz yo'q bo'lsa sizdagi muammoni batafsil yozing");

            await telegramBotClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: textFaq.ToString(),
                parseMode: ParseMode.Html,
                replyToMessageId: message.MessageId
                );
        }

        private async Task HandleTeachersCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleCoursesCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            ICoursesRepository coursesRepository  = new CoursesRepsotory();
            var courses = await coursesRepository.GetAll();

            var buttons = new List<InlineKeyboardButton>();

            foreach (var course in courses)
            {
                buttons.Add(new InlineKeyboardButton(course.Name){CallbackData = "course " + course.Id });
            }
           

            var reply = new InlineKeyboardMarkup(
                buttons
            );

            try{
                await telegramBotClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Kurslar! \n Birini tanlashingiz mumkin!",
                replyToMessageId: message.MessageId,
                replyMarkup: reply
                );
            }catch( Exception e )
            {
                System.Console.WriteLine(e.Message);
            }
        }

        private async Task HandleStartCommandAsync(ITelegramBotClient telegramBotClient, Message message, CancellationToken cancellationToken)
        {
            var buttonuz = new InlineKeyboardButton("Uz"){ CallbackData = "uz"};
            var buttonru = new InlineKeyboardButton("Ru"){ CallbackData = "re"};
            var buttonen = new InlineKeyboardButton("En"){ CallbackData = "en"};

            var reply = new InlineKeyboardMarkup(
                new InlineKeyboardButton[]
                {
                    buttonuz,
                    buttonru,
                    buttonen
                }
            );

            try{
                await telegramBotClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Tilni tanlang",
                replyToMessageId: message.MessageId,
                replyMarkup: reply
                );
            }catch( Exception e )
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
