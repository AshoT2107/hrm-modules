namespace BussinessLogic.Helpers
{
    public static class DateHelpers
    {
        public static bool IsValidDate(string input, string format, out DateTime parsedDate, int minYear = 1900, int maxYear = 2017)
        {
            if (DateTime.TryParseExact(input, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                int year = parsedDate.Year;

                // Validate the year within the specified range
                if (year >= minYear && year <= maxYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
