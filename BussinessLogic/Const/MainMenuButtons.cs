namespace BussinessLogic.Const
{
    public struct MainMenuButtons
    {
        public const string AboutUs = "🏢 About us";

        public const string Vacancies = "💼Vacancies";

        public const string Contact = "☎️Contact";
    }

    public struct VacanciesButton
    {
        public static List<string> CurrentVacancies = new List<string>()
        {
            "Junior Sales Consultant",
            "Customer Service Specialist",
        };
    }
    
    public struct HelperButtons
    {
        public const string Main = "🏠 Main menu";

        public const string Back = "🔙 Back";
    }

    public struct RegistrationButtons
    {
        public const string Contact = "⬆️Send contact 📲";

    }

    public struct OccupationButtons
    {
        public const string Student = "I'm a student";

        public const string NotStudent = "I don't study";

        public const string Worker = "I work, out of looking for a new job";

        public const string NotWorker = "I don't work";
    }

    public struct StudyFormButtons
    {
        public const string Online = "Online";

        public const string Offline = "Offline";

        public const string Evening = "Evening";
    }

    public struct EnglishLevelButtons
    {
        public const string Beginner = "Beginner";

        public const string Intermediate = "Intermediate";

        public const string Advanced = "Advanced";

        public const string Fluent = "Fluent";
    }

    public struct ApplicationProficientButtons
    {
        public const string Low = "Low";

        public const string Advanced = "Advanced";

        public const string Good = "Good";

        public const string Skip = "Skip";
    }

    public struct NightShiftButtons
    {
        public const string Yes = "Yes ✅";

        public const string No = "No ❌";
    }

    public struct VacancySourceButtons
    {
        public const string Sektor = "Sektor.uz";

        public const string Instagram_Facebook = "Instagram/Facebook";

        public const string OLX = "OLX.uz";

        public const string HH = "hh.uz";

        public const string Acquaintances = "Through acquaintances";

        public const string Other = "Other sources";
    }
}
