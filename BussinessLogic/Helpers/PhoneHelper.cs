namespace BussinessLogic.Helpers
{
    public static class PhoneHelper
    {
        public static Tuple<bool, string> IsValidUzbekistanNumber(string phoneNumber)
        {
            string validCodes = "90 91 93 94 95 97 98 99 88 71 33";
            if (phoneNumber.StartsWith("+998"))
            {
                phoneNumber = phoneNumber[4..];
                if (phoneNumber.Length == 9)
                {
                    if (!validCodes.Contains(phoneNumber.Substring(0, 2)))
                    {
                        return new(false, "Enter the correct information.");
                    }
                    phoneNumber = "+998" + phoneNumber;
                    return new Tuple<bool, string>(true, phoneNumber);
                }
                else if (phoneNumber.Length == 7)
                {
                    Console.WriteLine("What is the code of the number?");
                    string code = "90";
                    if (!validCodes.Contains(code))
                    {
                        return new(false, "Enter the correct information.");
                    }
                    phoneNumber = "+998" + code + phoneNumber;
                    return new Tuple<bool, string>(true, phoneNumber);
                }
                else
                {
                    phoneNumber = phoneNumber[4..];
                    if (!validCodes.Contains(phoneNumber[..2]))
                    {
                        return new(false, "Enter the correct information.");
                    }

                    if (phoneNumber.Length != 9)
                    {
                        return new(false, "Enter the correct information.");
                    }
                    phoneNumber = "+998" + phoneNumber;
                    return new Tuple<bool, string>(true, phoneNumber);
                }

            }
            else if (phoneNumber.StartsWith("998"))
            {

                if (phoneNumber.Length == 9)
                {
                    if (!validCodes.Contains(phoneNumber[..2]))
                    {
                        return new(false, "Enter the correct information.");
                    }
                    phoneNumber = "+998" + phoneNumber;
                    return new Tuple<bool, string>(true, phoneNumber);
                }
                else
                {
                    phoneNumber = phoneNumber[3..];
                    if (!validCodes.Contains(phoneNumber[..2]))
                    {
                        return new(false, "Enter the correct information.");
                    }

                    if (phoneNumber.Length != 9)
                    {
                        return new(false, "Enter the correct information.");
                    }
                    phoneNumber = "+998" + phoneNumber;
                    return new Tuple<bool, string>(true, phoneNumber);
                }
            }
            else if (phoneNumber.Length == 9)
            {
                if (!validCodes.Contains(phoneNumber.Substring(0, 2)))
                {
                    return new(false, "Enter the correct information.");
                }
                phoneNumber = "+998" + phoneNumber;
                return new Tuple<bool, string>(true, phoneNumber);
            }
            else
            {
                return new(false, "Enter the correct information.");
            }

        }
    }
}
