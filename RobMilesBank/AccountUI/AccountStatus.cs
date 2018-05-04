using System;
using AccountManagement;
using Dictionary;
using Validators;

namespace AccountUI
{
    public class AccountStatusUI
    {
        private DictionaryBank ourBank;
        public AccountStatusUI(DictionaryBank inBank)
        {
            ourBank = inBank;
        }
        public static void StatusScript(DictionaryBank ourBank)
        {
            string continueStatus;
            int inAccountNumber;
            IAccount chosenAccount;
            do
            {
                while (true)
                {
                    inAccountNumber = Validation.ValidateInt("\n-----Account Status-----\n" +
                        "\nPlease give the account number of the account you want to see. If you don't want to see the status of an account, type exit.", 0, 1000000);
                    if (inAccountNumber == -1)
                        return;
                    chosenAccount = ourBank.FindAccount(inAccountNumber);
                    if (chosenAccount == null)
                    {
                        Console.WriteLine("\nThere is no account with this account number. Are you sure you typed it correctly?");
                        continue;
                    }
                    break;

                }
                Console.WriteLine(chosenAccount.ToString());
                continueStatus = Validation.ThisOrThat("\nWould you like to the see status of another account [Y] or [N]?", "Please give your answer as [Y] or [N]", "y", "n");
            } while (continueStatus == "y");
        }
    }
}
