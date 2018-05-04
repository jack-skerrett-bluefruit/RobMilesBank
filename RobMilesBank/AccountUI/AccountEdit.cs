using System;
using AccountManagement;
using Dictionary;
using Validators;

namespace AccountUI
{
    public class AccountEditUI
    {
        private IAccount account;
        public AccountEditUI(IAccount inaccount)
        {
            account = inaccount;
        }
        public void DoEdit(IAccount account)
        {
            //do a check to see if account is babyaccount, then use AS keyword to cast it to a babyaccount
            //if(account is BabyAccount){var babyAccount = account as BabyAccount)}}
            string input;
            do
            {
                Console.WriteLine("\n-----Account Editing-----\n" +
                    "\nEditing account for {0}", account.GetName());
                Console.WriteLine("    Enter name to edit name");
                Console.WriteLine("    Enter pay to deposit funds");
                Console.WriteLine("    Enter draw to withdraw funds");
                Console.WriteLine("    Enter exit to stop editing this account");
                input = Validation.TrimLower(Console.ReadLine());
                switch (input)
                {
                    case "name":
                        AccountEditUI edit = new AccountEditUI(account);
                        edit.EditName();
                        break;
                    case "pay":
                        edit = new AccountEditUI(account);
                        edit.PayInFunds();
                        break;
                    case "draw":
                        edit = new AccountEditUI(account);
                        edit.WithdrawFunds();
                        break;
                }
            } while (input != "exit");
        }
        public static void EditScript(DictionaryBank ourBank)
        {
            IAccount chosenAccount;
            string continueEditing;
            do
            {
                while (true)
                {
                    int inAccountNumber = Validation.ValidateInt("\nPlease give the account number of the account you want to edit. If you don't want to edit an account, type exit.", 000000, 999999);
                    if (inAccountNumber == -1)
                    {
                        return;
                    }
                    chosenAccount = ourBank.FindAccount(inAccountNumber);
                    if (chosenAccount == null)
                        Console.WriteLine("\nThat account number does not exist in this bank");
                    break;
                }
                if (chosenAccount != null)
                {
                    AccountEditUI edit = new AccountEditUI(chosenAccount);
                    edit.DoEdit(chosenAccount);
                }
                continueEditing = Validation.ThisOrThat("\nWould you like to edit another account [Y] or [N]? ", "You have not entered [Y] or [N].", "y", "n");
            } while (continueEditing == "y");
        }
        public void EditName()
        {
            string newName;
            Console.WriteLine("\n-----Name Edit-----");
            newName = Validation.ValidateName("What would you like the name on this account to be?");
            account.SetName(newName);
        }
        public void PayInFunds()
        {
            decimal depositValue;
            Console.WriteLine("\n-----Deposit Funds-----");
            while (true)
            {
                depositValue = Validation.ValidateDecimal("Enter how much money you would like to deposit: ", 0, 10000);
                account.PayInFunds(depositValue);
                break;
            }
        }
        public void WithdrawFunds()
        {
            decimal withdrawValue;
            Console.WriteLine("\n-----Withdraw Funds-----");
            while (true)
            {
                withdrawValue = Validation.ValidateDecimal("Enter how much money you would like to withdraw: ", 0, 1000);
                if (account.WithdrawFunds(withdrawValue))
                    break;
                else
                    Console.WriteLine("\nThat is more money than you have in the account.\n");
            }
        }
        //public void EditParentName()
        //{
        //    string newParentName;
        //    Console.WriteLine("\n-----Parent Name Edit-----");
        //    newParentName = Validation.ValidateName("What would you like the parent name on this account to be?");
        //    account.SetParentName(newParentName);
        //}

    }
}
