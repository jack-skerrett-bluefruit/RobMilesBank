using System;
using AccountManagement;
using Dictionary;
using Validators;

namespace AccountUI
{
    public class AccountDeleteUI
    {
        private DictionaryBank ourBank;
        public AccountDeleteUI(DictionaryBank inBank)
        {
            ourBank = inBank;
        }
        public static void DeleteScript(DictionaryBank ourBank)
        {
            string continueDeleting;
            int inAccountNumber;
            do
            {
                while (true)
                {
                    inAccountNumber = Validation.ValidateInt("\n-----Account Deletion-----\n" +
                        "\nPlease give the account number of the account you want to delete. If you don't want to delete an account, type exit.", 0, 1000000);
                    if (inAccountNumber == -1)
                        return;
                    string answer = Validation.ThisOrThat("\nAre you sure you want to delete this account, this cannot be undone. Type [Y] or [N].", "Please give your answer as [Y] or [N]", "y", "n");
                    if (answer == "n")
                        break;
                    ourBank.DeleteAccount(inAccountNumber);
                    break;
                }
                continueDeleting = Validation.ThisOrThat("\nWould you like to delete another account [Y] or [N]?", "Please give your answer as [Y] or [N]", "y", "n");
            } while (continueDeleting == "y");
        }
    }
}
