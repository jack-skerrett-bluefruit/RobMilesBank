using System;
using AccountManagement;
using Dictionary;
using Validators;



namespace AccountUI
{
    public class AccountCreateUI
    {
        private DictionaryBank ourBank;
        public AccountCreateUI(DictionaryBank inBank)
        {
            ourBank = inBank;
        }
        public static void CreateScript(DictionaryBank ourBank)
        {
            string continueCreating;
            do
            {
                Console.WriteLine("\n-----Account Creation-----\n" +
                    "\nWhat type of account would you like to create?" +
                    "\n      Type customer for a new customer account" +
                    "\n      Type baby for a new baby account");
                string input = Validation.TrimLower(Console.ReadLine());
                switch (input)
                {
                    case "customer":
                        AccountCreateUI create = new AccountCreateUI(ourBank);
                        create.CreateCustomerAccount();
                        break;
                    case "baby":
                        create = new AccountCreateUI(ourBank);
                        create.CreateBabyAccount();
                        break;
                    default:
                        Console.WriteLine("You did not select a baby or customer account");
                        break;
                }
                continueCreating = Validation.ThisOrThat("\nWould you like to create another account [Y] or [N]?", "You have not entered [Y] or [N]", "y", "n");
            } while (continueCreating == "y");
        }
        private void CreateCustomerAccount()
        {
            string name = Validation.ValidateName("\nWhat would you like the name on the account to be?");
            decimal balance = Validation.ValidateDecimal("How much money would you like initially deposited into the account?", 0, 10000);
            CustomerAccount newAccount = new CustomerAccount(name, balance);
            Console.WriteLine("\nAccount summary: \n" + newAccount.ToString());
            ourBank.StoreAccount(newAccount);
        }
        private void CreateBabyAccount()
        {
            string name = Validation.ValidateName("\nWhat would you like the name on the account to be?");
            decimal balance = Validation.ValidateDecimal("How much money would you like initially deposited into the account?", 0, 10000);
            string parentName = Validation.ValidateName("What would you like the parent name on the account to be?");
            BabyAccount newAccount = new BabyAccount(name, balance, parentName);
            Console.WriteLine("\nAccount summary: \n" + newAccount.ToString());
            ourBank.StoreAccount(newAccount);
        }
    }
}
