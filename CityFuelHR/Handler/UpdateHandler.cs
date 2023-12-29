using BussinessLogic.Const;
using BussinessLogic.Helpers;
using BussinessLogic.Steps;
using DataAccess.Data;
using DataAccess.Entities;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CityFuelHR.Handler
{
    public class UpdateHandler : IUpdateHandler
    {
        private string? fullName;
        private string? birthDate;
        private string? phone;
        private string? vacancyType;
        private string? institution;
        private string? studyForm;
        private string? englishLevel;
        private string? appProficiency;
        private string? media;
        private bool nightShift;
        private string? resume;
        private string? source;
        private bool isStudent = false;

        private readonly HRContext context = new HRContext();

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage, cancellationToken);

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Message is not { } message)
                return;

            var chatId = message.Chat.Id;


            if (MenuAction.Step == Menu.Vacancies && ClientAction.Step == Client.Video)
            {
                if (message.Video != null)
                {
                    var video = message.Video;
                    if (video.FileSize <= 20 * 1024 * 1024)
                    {
                        var file = await botClient.GetFileAsync(video.FileId, cancellationToken: cancellationToken);
                        var filePath = file.FilePath;

                        if (filePath != null)
                        {
                            string destinationFolder = "Videos";

                            if (!Directory.Exists(destinationFolder))
                            {
                                Directory.CreateDirectory(destinationFolder);
                            }

                            media = Path.Combine(destinationFolder, Path.GetFileName(filePath));


                            await Task.Factory.StartNew(async () =>
                            {
                                try
                                {
                                    using var fileStream = new FileStream(media, FileMode.Create);
                                    await botClient.DownloadFileAsync(filePath, fileStream);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error downloading file: {ex.Message}", cancellationToken);
                                    return;
                                }
                            }, TaskCreationOptions.LongRunning);

                            await botClient.SendTextMessageAsync(chatId, @"What level of the software applications you are proficient with", replyMarkup: ButtonHelpers.ApplicationProficients(), cancellationToken: cancellationToken);

                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.App;
                            return;
                        }

                        else
                        {
                            if (message.Text == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.EnglishLevel;
                                return;
                            }

                            if (message.Text == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            await botClient.SendTextMessageAsync(chatId, "Enter the correct information.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                            await botClient.SendTextMessageAsync(chatId, Briefing.VideoDescription, replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.Video;
                            return;
                        }
                    }

                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Enter the correct format.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                        await botClient.SendTextMessageAsync(chatId, Briefing.VideoDescription, replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Vacancies;
                        ClientAction.Step = Client.Video;
                        return;
                    }
                }

                else if (message.Document != null && message.Document.MimeType == "video/mp4")
                {
                    var document = message.Document;

                    if (document.FileSize <= 20 * 1024 * 1024)
                    {
                        var file = await botClient.GetFileAsync(document.FileId, cancellationToken: cancellationToken);
                        var filePath = file.FilePath;

                        if (filePath != null)
                        {
                            string destinationFolder = "Videos";

                            if (!Directory.Exists(destinationFolder))
                            {
                                Directory.CreateDirectory(destinationFolder);
                            }

                            media = Path.Combine(destinationFolder, Path.GetFileName(filePath));


                            await Task.Factory.StartNew(async () =>
                            {
                                try
                                {
                                    using var fileStream = new FileStream(media, FileMode.Create);
                                    await botClient.DownloadFileAsync(filePath, fileStream);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error downloading file: {ex.Message}", cancellationToken);
                                    return;
                                }
                            }, TaskCreationOptions.LongRunning);

                            await botClient.SendTextMessageAsync(chatId, @"What level of the software applications you are proficient with", replyMarkup: ButtonHelpers.ApplicationProficients(), cancellationToken: cancellationToken);

                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.App;
                            return;
                        }

                        else
                        {
                            if (message.Text == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.EnglishLevel;
                                return;
                            }

                            if (message.Text == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            await botClient.SendTextMessageAsync(chatId, "Enter the correct information.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                            await botClient.SendTextMessageAsync(chatId, Briefing.VideoDescription, replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.Video;
                            return;
                        }
                    }

                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Enter the correct format.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                        await botClient.SendTextMessageAsync(chatId, Briefing.VideoDescription, replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Vacancies;
                        ClientAction.Step = Client.Video;
                        return;
                    }
                }

                else
                {
                    if (message.Text == HelperButtons.Back)
                    {
                        await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Vacancies;
                        ClientAction.Step = Client.EnglishLevel;
                        return;
                    }

                    if (message.Text == HelperButtons.Main)
                    {
                        await ButtonHelpers.SendMainMenus(botClient, chatId);
                        return;
                    }

                    await botClient.SendTextMessageAsync(chatId, "Enter the correct information.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                    await botClient.SendTextMessageAsync(chatId, Briefing.VideoDescription, replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                    MenuAction.Step = Menu.Vacancies;
                    ClientAction.Step = Client.Video;
                    return;
                }
            }

            if (MenuAction.Step == Menu.Vacancies && ClientAction.Step == Client.CV)
            {
                var document = message.Document;

                if (document != null)
                {
                    var file = await botClient.GetFileAsync(document.FileId, cancellationToken: cancellationToken);
                    var filePath = file.FilePath;

                    if (filePath != null)
                    {
                        if (!document.FileName!.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) &&
                            !document.FileName!.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                        {
                            await botClient.SendTextMessageAsync(chatId, "Enter the correct information.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                            await botClient.SendTextMessageAsync(chatId, "Send your CV", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.WorkTime;
                            return;
                        }

                        string destinationFolder = "Resumes";

                        if (!Directory.Exists(destinationFolder))
                        {
                            Directory.CreateDirectory(destinationFolder);
                        }

                        resume = Path.Combine(destinationFolder, Path.GetFileName(filePath));

                        await Task.Factory.StartNew(async () =>
                        {
                            try
                            {
                                using var fileStream = new FileStream(resume, FileMode.Create);
                                await botClient.DownloadFileAsync(filePath, fileStream);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error downloading file: {ex.Message}", cancellationToken);
                                return;
                            }
                        }, TaskCreationOptions.LongRunning);

                        await botClient.SendTextMessageAsync(chatId, @"❓ How did you find out about the vacancy?", replyMarkup: ButtonHelpers.VacancySources(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Vacancies;
                        ClientAction.Step = Client.Network;
                        return;
                    }
                }

                else
                {
                    if (message.Text == HelperButtons.Back)
                    {
                        await botClient.SendTextMessageAsync(chatId, "Tell us if you can work at night shift.", replyMarkup: ButtonHelpers.NightShifts(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Vacancies;
                        ClientAction.Step = Client.WorkTime;
                        return;
                    }

                    if (message.Text == HelperButtons.Main)
                    {
                        await ButtonHelpers.SendMainMenus(botClient, chatId);
                        return;
                    }

                    await botClient.SendTextMessageAsync(chatId, "Enter the correct information.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                    await botClient.SendTextMessageAsync(chatId, "Send your CV", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                    MenuAction.Step = Menu.Vacancies;
                    ClientAction.Step = Client.WorkTime;
                    return;
                }

            }

            var messageText = message.Contact != null ? message.Contact.PhoneNumber : message.Text;

            if (messageText == null)
                return;

            switch (messageText) 
            {
                case "/start": await ButtonHelpers.SendMainMenus(botClient, chatId); 
                    return;

                case HelperButtons.Main: await ButtonHelpers.SendMainMenus(botClient, chatId); 
                    return;

                case "/contactus" or MainMenuButtons.Contact:
                    await botClient.SendTextMessageAsync(chatId: chatId, text: Briefing.ContactUs, replyMarkup: ButtonHelpers.MainMenus(), cancellationToken: cancellationToken);

                    MenuAction.Step = Menu.Start;
                    ClientAction.Step = Client.Start; 
                    return;

                case "/vacancies" or MainMenuButtons.Vacancies:
                    await botClient.SendTextMessageAsync(chatId, "Let's start filling  your resume", cancellationToken: cancellationToken);
                    await botClient.SendTextMessageAsync(chatId, "Choose one of the vacancies", replyMarkup: ButtonHelpers.Vacancies(), cancellationToken: cancellationToken);

                    MenuAction.Step = Menu.Vacancies;
                    ClientAction.Step = Client.Vacancies;
                    return;

                case "/aboutus" or MainMenuButtons.AboutUs:
                    await botClient.SendPhotoAsync(chatId: chatId, photo: InputFile.FromStream(new MemoryStream(System.IO.File.ReadAllBytes("aboutus.jpg"))), caption: Briefing.AboutUsCaption, replyMarkup: ButtonHelpers.MainMenus(), cancellationToken: cancellationToken);

                    MenuAction.Step = Menu.Start;
                    ClientAction.Step = Client.Start;
                    return;
            }

            switch (MenuAction.Step)
            {
                case Menu.Start:

                    if (messageText == "/start")
                    {
                        await ButtonHelpers.SendMainMenus(botClient, chatId);
                    }

                    if (messageText == "/contactus" || messageText == MainMenuButtons.Contact)
                    {
                        await botClient.SendTextMessageAsync(chatId: chatId, text: Briefing.ContactUs, replyMarkup: ButtonHelpers.MainMenus(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Start;
                        ClientAction.Step = Client.Start;
                    }

                    if (messageText == "/aboutus" || messageText == MainMenuButtons.AboutUs)
                    {
                        await botClient.SendPhotoAsync(chatId: chatId, photo: InputFile.FromStream(new MemoryStream(System.IO.File.ReadAllBytes("aboutus.jpg"))), caption: Briefing.AboutUsCaption, replyMarkup: ButtonHelpers.MainMenus(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Start;
                        ClientAction.Step = Client.Start;
                    }

                    if (messageText == "/vacancies" || messageText == MainMenuButtons.Vacancies)
                    {
                        await botClient.SendTextMessageAsync(chatId, "Let's start filling  your resume", cancellationToken: cancellationToken);
                        await botClient.SendTextMessageAsync(chatId, "Choose one of the vacancies", replyMarkup: ButtonHelpers.Vacancies(), cancellationToken: cancellationToken);

                        MenuAction.Step = Menu.Vacancies;
                        ClientAction.Step = Client.Vacancies;
                    }

                    break;

                case Menu.Vacancies:
                    switch (ClientAction.Step)
                    {
                        case Client.Vacancies:
                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, Briefing.Introduction, replyMarkup: ButtonHelpers.MainMenus(), cancellationToken: cancellationToken);

                                ClientAction.Step = Client.Start;
                                MenuAction.Step = Menu.Start;

                                return;
                            }
                             
                            if (VacanciesButton.CurrentVacancies.Any(x=>x == messageText))
                            {
                                await botClient.SendTextMessageAsync(chatId, @$"✅""Etherial Group"" LLC announces a vacancy for the position of {messageText}✅

💥Requirements:💥

📌English Level: Advanced 6.5+ IELTS;
📌Computer Skills: Microsoft Office, Google sheets;
📌Excellent communication and interpersonal skills;
📌Analytical and decision-making skills.

❗️Working conditions:❗️

📌5-day work week according to American time EST (19:00-03:00 Tashkent time)
📌Comfortable open space office in the center of Tashkent
📌Official registration
📌Opportunity for growth and professional development
📌A supportive team environment that values innovation and creativity
💵  Fixed salary + bonuses", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "👤 Enter your full name (First and Last name)", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                vacancyType = messageText;

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.FullName;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Entered incorrect information", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "Choose one of the vacancies", replyMarkup: ButtonHelpers.Vacancies(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Vacancies;
                            }

                            break;

                        case Client.FullName:
                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "Choose one of the vacancies", replyMarkup: ButtonHelpers.Vacancies(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Vacancies;

                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            fullName = messageText;

                            await botClient.SendTextMessageAsync(chatId, "Date of birth (ex. 12.03.1995)", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.Date;
                            break;

                        case Client.Date:
                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "👤 Enter your full name (First and Last name)", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.FullName;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            var dateFormat = "dd.MM.yyyy";

                            if (DateHelpers.IsValidDate(messageText, dateFormat,out DateTime date))
                            {
                                birthDate = date.ToString($"{dateFormat}");

                                await botClient.SendTextMessageAsync(chatId, "📱 Enter your contact phone number (example: +998XXXXXXXXX):", replyMarkup: ButtonHelpers.Contact(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Contact;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Enter the correct information.", cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "Date of birth (ex. 12.03.1995)", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Date;
                            }

                            break;

                        case Client.Contact:
                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "Date of birth (ex. 12.03.1995)", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Date;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            var (isValid, phoneMessage) = PhoneHelper.IsValidUzbekistanNumber(messageText);

                            if (isValid)
                            {
                                phone = phoneMessage;

                                await botClient.SendTextMessageAsync(chatId, "What do you do?", replyMarkup: ButtonHelpers.Occupations(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Occupations;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, phoneMessage, replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "📱 Enter your contact phone number (example: +998XXXXXXXXX):", replyMarkup: ButtonHelpers.Contact(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Contact;
                            }

                            break;

                        case Client.Occupations:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "📱 Enter your contact phone number (example: +998XXXXXXXXX):", replyMarkup: ButtonHelpers.Contact(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Contact;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            if (messageText == OccupationButtons.Student)
                            {
                                isStudent = true;

                                await botClient.SendTextMessageAsync(chatId, "Name of the educational institution", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Institution;

                                return;
                            }

                            if (messageText == OccupationButtons.NotStudent ||
                           messageText == OccupationButtons.Worker ||
                           messageText == OccupationButtons.NotWorker)
                            {
                                isStudent = false;

                                await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.EnglishLevel;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Wrong choice", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "What do you do?", replyMarkup: ButtonHelpers.Occupations(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Occupations;
                            }

                            break;

                        case Client.Institution:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "What do you do?", replyMarkup: ButtonHelpers.Occupations(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Occupations;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            institution = messageText;

                            await botClient.SendTextMessageAsync(chatId, "Form of study", replyMarkup: ButtonHelpers.StudyForms(), cancellationToken: cancellationToken);
                            MenuAction.Step = Menu.Vacancies;
                            ClientAction.Step = Client.StudyForm;

                            break;

                        case Client.StudyForm:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "Name of the educational institution", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Institution;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            if (messageText == StudyFormButtons.Online || messageText == StudyFormButtons.Offline || messageText == StudyFormButtons.Evening)
                            {
                                studyForm = messageText;

                                await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);
                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.EnglishLevel;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Wrong choice", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "Form of study", replyMarkup: ButtonHelpers.StudyForms(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.StudyForm;
                            }

                            break;

                        case Client.EnglishLevel:

                            if (messageText == HelperButtons.Back)
                            {
                                if (isStudent)
                                {
                                    await botClient.SendTextMessageAsync(chatId, "Form of study", replyMarkup: ButtonHelpers.StudyForms(), cancellationToken: cancellationToken);

                                    MenuAction.Step = Menu.Vacancies;
                                    ClientAction.Step = Client.StudyForm;
                                    return;
                                }
                                else
                                {
                                    await botClient.SendTextMessageAsync(chatId, "What do you do?", replyMarkup: ButtonHelpers.Occupations(), cancellationToken: cancellationToken);

                                    MenuAction.Step = Menu.Vacancies;
                                    ClientAction.Step = Client.Occupations;
                                    return;
                                }
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);
                                return;
                            }

                            if (messageText == EnglishLevelButtons.Beginner ||
                               messageText == EnglishLevelButtons.Intermediate ||
                               messageText == EnglishLevelButtons.Advanced ||
                               messageText == EnglishLevelButtons.Fluent)
                            {

                                englishLevel = messageText;

                                await botClient.SendTextMessageAsync(chatId, @"1. Tell us about yourself / background
2. Tell us about your previous jobs / have you ever had a customer service or sales job
3. Tell us if you can work at night shift.
4. Tell us why you think you fit this sales job.

❗️Please send us a short video answering following question❗️
max 20 Mb", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Video;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Wrong choice", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.EnglishLevel;
                            }

                            break;

                        case Client.Video:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "EN What is your level of English?", replyMarkup: ButtonHelpers.EnglishLevels(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.EnglishLevel;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);

                                return;
                            }

                            break;

                        case Client.App:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, @"1. Tell us about yourself / background
2. Tell us about your previous jobs / have you ever had a customer service or sales job
3. Tell us if you can work at night shift.
4. Tell us why you think you fit this sales job.

❗️Please send us a short video answering following question❗️
max 20 Mb", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Video;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);

                                return;
                            }

                            if (messageText == ApplicationProficientButtons.Low ||
                               messageText == ApplicationProficientButtons.Advanced ||
                               messageText == ApplicationProficientButtons.Good ||
                               messageText == ApplicationProficientButtons.Skip)
                            {
                                appProficiency = messageText;

                                await botClient.SendTextMessageAsync(chatId, "Tell us if you can work at night shift.", replyMarkup: ButtonHelpers.NightShifts(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.WorkTime;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Wrong choice", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, @"What level of the software applications you are proficient with", replyMarkup: ButtonHelpers.ApplicationProficients(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.App;
                            }

                            break;

                        case Client.WorkTime:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, @"What level of the software applications you are proficient with", replyMarkup: ButtonHelpers.ApplicationProficients(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.App;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);

                                return;
                            }

                            if (messageText == NightShiftButtons.Yes || messageText == NightShiftButtons.No)
                            {
                                nightShift = messageText == NightShiftButtons.Yes;

                                await botClient.SendTextMessageAsync(chatId, "Send your CV", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.CV;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Wrong choice", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, "Tell us if you can work at night shift.", replyMarkup: ButtonHelpers.NightShifts(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.WorkTime;
                            }

                            break;

                        case Client.CV:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "Tell us if you can work at night shift.", replyMarkup: ButtonHelpers.NightShifts(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.WorkTime;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);

                                return;
                            }

                            break;

                        case Client.Network:

                            if (messageText == HelperButtons.Back)
                            {
                                await botClient.SendTextMessageAsync(chatId, "Send your CV", replyMarkup: ButtonHelpers.Helpers(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.CV;
                                return;
                            }

                            if (messageText == HelperButtons.Main)
                            {
                                await ButtonHelpers.SendMainMenus(botClient, chatId);

                                return;
                            }

                            if (messageText == VacancySourceButtons.Sektor ||
                                messageText == VacancySourceButtons.Instagram_Facebook ||
                                messageText == VacancySourceButtons.OLX ||
                                messageText == VacancySourceButtons.HH ||
                                messageText == VacancySourceButtons.Acquaintances ||
                                messageText == VacancySourceButtons.Other)
                            {
                                source = messageText;

                                await context.Employees.AddAsync(new Employee()
                                {
                                    Id = Guid.NewGuid(),
                                    FullName = fullName,
                                    Birthdate = birthDate,
                                    Phone = phone,
                                    VacancyType = vacancyType,
                                    Institution = institution,
                                    StudyForm = studyForm,
                                    EnglishLevel = englishLevel,
                                    AppProficiency = appProficiency,
                                    Media = media,
                                    NightShift = nightShift,
                                    Resume = resume,
                                    Source = source,
                                    IsStudent = isStudent,
                                    RegisterTime = DateTime.UtcNow
                                }, cancellationToken);

                                await context.SaveChangesAsync(cancellationToken);


                                await botClient.SendTextMessageAsync(chatId, Briefing.Gratitude, replyMarkup: ButtonHelpers.MainMenus(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Start;
                                ClientAction.Step = Client.Start;
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(chatId, "Wrong choice", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                                await botClient.SendTextMessageAsync(chatId, @"❓ How did you find out about the vacancy?", replyMarkup: ButtonHelpers.VacancySources(), cancellationToken: cancellationToken);

                                MenuAction.Step = Menu.Vacancies;
                                ClientAction.Step = Client.Network;
                            }

                            break;
                    }

                    break;
            }

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.", cancellationToken);
        }
    }
}
