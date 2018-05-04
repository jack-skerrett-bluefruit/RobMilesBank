using System;


namespace Validators
{
    public class Validation
    {
        public static string ValidateName(string prompt)
        {
            string trimmedName;
            while (true)
            {
                Console.WriteLine(prompt);
                string inName = Console.ReadLine();
                trimmedName = inName.Trim();
                if (trimmedName == null)
                {
                    Console.WriteLine("\nYou have entered a null value, please enter another name.");
                    continue;
                }
                if (trimmedName.Length == 0)
                {
                    Console.WriteLine("\nYou didn't enter any text, please enter a name");
                    continue;
                };
                break;
            }
            return trimmedName;
        }
        public static int ValidateInt(string prompt, int min, int max)
        {
            if (min >= max)
            {
                //some kind of exception 
            }
            string userValue;
            int parsedUserValue;
            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    userValue = Console.ReadLine();
                    userValue = Validation.TrimLower(userValue);
                    if (userValue == "exit")
                        return -1; ;
                    parsedUserValue = int.Parse(userValue);
                    if (parsedUserValue < min || parsedUserValue > max)
                    {
                        Console.WriteLine("Your value must fall between {0} (min) and {1} (max)", min, max);
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("You must enter a numeric value between {0} (min) and {1} (max)", min, max);
                    continue;
                }
            };
            return parsedUserValue;
        }
        public static decimal ValidateDecimal(string prompt, int min, int max)
        {
            if (min >= max)
            {
                //some kind of exception
            }
            string userValue;
            decimal parsedUserValue;
            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    userValue = Console.ReadLine();
                    parsedUserValue = decimal.Parse(userValue);
                    if (parsedUserValue < min || parsedUserValue > max)
                    {
                        Console.WriteLine("Your value must fall between {0} (min) and {1} (max)", min, max);
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("You must enter a numeric value between {0} (min) and {1} (max)", min, max);
                }
            }
            return parsedUserValue;
        }
        public static string ThisOrThat(string prompt, string errorMsg, string answerOne, string answerTwo)
        {
            if (answerOne == answerTwo)
            {
                throw new Exception("The two answers are the same.");
            }
            string answer;
            while (true)
            {
                Console.WriteLine(prompt);
                answer = Validation.TrimLower(Console.ReadLine());
                if (answer != answerOne && answer != answerTwo)
                {
                    Console.WriteLine(errorMsg);
                    continue;
                }
                return answer;
            }
        }
        public static string TrimLower(string toFormat)
        {
            toFormat = toFormat.Trim();
            toFormat = toFormat.ToLower();
            return toFormat;
        }
    }
}
