using BussinessLogic.Const;
using BussinessLogic.Steps;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace BussinessLogic.Helpers
{
    public static class ButtonHelpers
    {
        public static async Task SendMainMenus(ITelegramBotClient botClient, long chatId)
        {
            await botClient.SendTextMessageAsync(chatId, Briefing.Introduction, replyMarkup: MainMenus());

            ClientAction.Step = Client.Start;
            MenuAction.Step = Menu.Start;
        }

        public static ReplyKeyboardMarkup MainMenus()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                                         [
                                                             new KeyboardButton(MainMenuButtons.AboutUs),
                                                         ],
                                                         [
                                                             new KeyboardButton(MainMenuButtons.Vacancies)
                                                         ],
                                                         [
                                                             new KeyboardButton(MainMenuButtons.Contact)
                                                         ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup Helpers()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                                                [
                                                                    new KeyboardButton(HelperButtons.Main),
                                                                    new KeyboardButton(HelperButtons.Back),
                                                                ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup Contact()
        {
            return new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
            {
                new()
                {
                    new KeyboardButton(RegistrationButtons.Contact)
                    {
                        RequestContact = true
                    }
                },
                new()
                {
                    new KeyboardButton(HelperButtons.Main),
                    new KeyboardButton(HelperButtons.Back),
                }
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup Vacancies()
        {
            var buttonRows = new List<List<KeyboardButton>>();
            var buttonText = VacanciesButton.CurrentVacancies;

            for (int i = 0; i < buttonText.Count; i += 2)
            {
                if (i + 1 < buttonText.Count)
                {
                    var keyboards = new List<KeyboardButton>()
                    {
                        new KeyboardButton(buttonText[i]),
                        new KeyboardButton(buttonText[i+1])
                    };
                    buttonRows.Add(keyboards);
                }
                else
                {
                    var keyboards = new List<KeyboardButton>()
                    {
                        new KeyboardButton(buttonText[i]),
                    };
                    buttonRows.Add(keyboards);
                }
            }

            buttonRows.Add(new List<KeyboardButton>()
                    {
                        new KeyboardButton(HelperButtons.Main),
                        new KeyboardButton(HelperButtons.Back),
                    });

            return new ReplyKeyboardMarkup(buttonRows)
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup ApplicationProficients()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                                        [
                                                            new KeyboardButton(ApplicationProficientButtons.Low),
                                                            new KeyboardButton(ApplicationProficientButtons.Advanced),
                                                        ],
                                                        [
                                                            new KeyboardButton(ApplicationProficientButtons.Good)
                                                        ],
                                                        [
                                                            new KeyboardButton(ApplicationProficientButtons.Skip)
                                                        ],
                                                        [
                                                            new KeyboardButton(HelperButtons.Main),
                                                            new KeyboardButton(HelperButtons.Back),
                                                        ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup EnglishLevels()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                    [
                                        new KeyboardButton(EnglishLevelButtons.Beginner),
                                        new KeyboardButton(EnglishLevelButtons.Intermediate),
                                    ],
                                    [
                                        new KeyboardButton(EnglishLevelButtons.Advanced),
                                        new KeyboardButton(EnglishLevelButtons.Fluent),
                                    ],
                                    [
                                        new KeyboardButton(HelperButtons.Main),
                                        new KeyboardButton(HelperButtons.Back),
                                    ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup VacancySources()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                                                        [
                                                                            new KeyboardButton(VacancySourceButtons.Sektor),
                                                                            new KeyboardButton(VacancySourceButtons.Instagram_Facebook),
                                                                        ],
                                                                        [
                                                                            new KeyboardButton(VacancySourceButtons.OLX),
                                                                            new KeyboardButton(VacancySourceButtons.HH),
                                                                        ],
                                                                        [
                                                                            new KeyboardButton(VacancySourceButtons.Acquaintances),
                                                                            new KeyboardButton(VacancySourceButtons.Other)
                                                                        ],
                                                                        [
                                                                            new KeyboardButton(HelperButtons.Main),
                                                                            new KeyboardButton(HelperButtons.Back),
                                                                        ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup NightShifts()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                    [
                                        new KeyboardButton(NightShiftButtons.Yes),
                                        new KeyboardButton(NightShiftButtons.No),
                                    ],
                                    [
                                        new KeyboardButton(HelperButtons.Main),
                                        new KeyboardButton(HelperButtons.Back),
                                    ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup Occupations()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                    [
                                        new KeyboardButton(OccupationButtons.Student),
                                        new KeyboardButton(OccupationButtons.NotStudent),
                                    ],
                                    [
                                        new KeyboardButton(OccupationButtons.Worker),
                                        new KeyboardButton(OccupationButtons.NotWorker),
                                    ],
                                    [
                                        new KeyboardButton(HelperButtons.Main),
                                        new KeyboardButton(HelperButtons.Back),
                                    ]})
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup StudyForms()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]{
                                    [
                                        new KeyboardButton(StudyFormButtons.Online),
                                        new KeyboardButton(StudyFormButtons.Offline),
                                    ],
                                    [
                                        new KeyboardButton(StudyFormButtons.Evening)
                                    ],
                                    [
                                        new KeyboardButton(HelperButtons.Main),
                                        new KeyboardButton(HelperButtons.Back),
                                    ]})
            {
                ResizeKeyboard = true
            };
        }
    }
}
