using System;
using System.Collections.Generic;
using AccountManagement;



namespace RobMilesBank
{
    public class DictionaryBank 
    {
        Dictionary<int, IAccount> accountDictionary = new Dictionary<int, IAccount>();
        public IAccount FindAccount(int accountNumber)
        {
            if (accountDictionary.ContainsKey(accountNumber) == true)
                return accountDictionary[accountNumber];
            else
                return null;
        }
        public bool AccountsInBank()
        {
            if (accountDictionary.Count == 0)
                return false;
            return true;
        }
        public bool StoreAccount(IAccount account)
        {
            if (accountDictionary.ContainsKey(account.GetAccountNumber()) == true)
                return false;

            accountDictionary.Add(account.GetAccountNumber(), account);
            return true;
        }

        public void Save(System.IO.TextWriter textOut)
        {
            textOut.WriteLine(accountDictionary.Count);
            foreach (CustomerAccount account in accountDictionary.Values)
            {
                textOut.WriteLine(account.GetType().Name);
                account.Save(textOut);
            }
        }

        public bool Save(string filename)
        {
            System.IO.TextWriter textOut = null;
            try
            {
                textOut = new System.IO.StreamWriter(filename);
                Save(textOut);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }
            return true;
        }

        public static DictionaryBank Load(System.IO.TextReader textIn)
        {
            DictionaryBank result = new DictionaryBank();
            string countString = textIn.ReadLine();
            int count = int.Parse(countString);

            for (int i = 0; i < count; i++)
            {
                string className = textIn.ReadLine();
                IAccount account =
                    AccountFactory.MakeAccount(className, textIn);
                result.StoreAccount(account);
            }
            return result;
        }

        public static DictionaryBank Load(string filename)
        {
            System.IO.TextReader textIn = null;
            DictionaryBank result = null;
            try
            {
                textIn = new System.IO.StreamReader(filename);
                result = DictionaryBank.Load(textIn);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (textIn != null) textIn.Close();
            }

            return result;
        }

    }

    class AccountFactory
    {
        public static IAccount MakeAccount(
            string name, System.IO.TextReader textIn)
        {
            switch (name)
            {
                case "CustomerAccount":
                    return new CustomerAccount(textIn);
                case "BabyAccount":
                    return new BabyAccount(textIn);
                default:
                    return null;
            }
        }
    }
    public class AccountCreateUI
    {
        private DictionaryBank ourBank;
        public AccountCreateUI(DictionaryBank inBank)
        {
            ourBank = inBank;
        }
        public static  void CreateScript(DictionaryBank ourBank)
        {
            string continueCreating;
            do
            {
                Console.WriteLine("What type of account would you like to create?" +
                    "\n      Type customer for a new customer account" +
                    "\n      Type baby for a new baby account");
                string input = AccountEditUI.TrimLower(Console.ReadLine());
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
                        Console.WriteLine("You did not select a baby or customer account.");
                            break;
                }
                continueCreating = AccountEditUI.ThisOrThat("\nWould you like to create an account [Y] or [N]", "You have not entered [Y] or [N]", "y", "n");
            } while (continueCreating == "y");

        }
        private void CreateCustomerAccount()
        {
            Console.WriteLine("\nWhat would you like the name on the account to be (this can be edited later)?");
            string name = CustomerAccount.ValidateName(Console.ReadLine());
            decimal balance = CustomerAccount.ValidateDecimal("How much money would you like initially deposited into the account?", 0, 10000);
            CustomerAccount newAccount = new CustomerAccount(name, balance);
            ourBank.StoreAccount(newAccount);
        }
        private void CreateBabyAccount()
        {
            Console.WriteLine("\nWhat would you like the name on the account to be (this can be edited later)?");
            string name = CustomerAccount.ValidateName(Console.ReadLine());
            decimal balance = CustomerAccount.ValidateDecimal("How much money would you like initially deposited into the account?", 0, 1000);
            Console.WriteLine("What would you like the parent's name on the account to be (this can be edited later)?"); //this cannot be edited later yet
            string parentName = CustomerAccount.ValidateName(Console.ReadLine());
            BabyAccount newAccount = new BabyAccount(name, balance, parentName);
            ourBank.StoreAccount(newAccount);
        }
    }
    public class AccountEditUI
    {
        private IAccount account;
        public AccountEditUI(IAccount inaccount)
        {
            account = inaccount;
        }
        public void DoEdit(IAccount account)// need to be careful with this because at the minute I can accept a babyaccount to edit but there is no option to edit the baby account specific things (parent name)
        {
            string input;
            do
            {
                Console.WriteLine("\nEditing account for {0}", account.GetName());
                Console.WriteLine("    Enter name to edit name");
                Console.WriteLine("    Enter pay to pay in funds");
                Console.WriteLine("    Enter draw to draw out funds");
                Console.WriteLine("    Enter exit to exit editing mode");
                input = TrimLower(Console.ReadLine());
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
                    int chosenAccountNumber = Decimal.ToInt32(CustomerAccount.ValidateDecimal("\nWhich account would you like to edit (please give the account number)?", 100000, 1000000));
                    chosenAccount = ourBank.FindAccount(chosenAccountNumber);
                    if (chosenAccount == null)
                    {
                        Console.WriteLine("\nThat name does not exist in this bank. Are you sure you spelt the name correctly?");
                        continue;
                    }
                    break;
                }

                AccountEditUI edit = new AccountEditUI(chosenAccount);
                edit.DoEdit(chosenAccount);
                continueEditing = ThisOrThat("\nWould you like to continue editing [Y] or [N]? ", "You have not entered [Y] or [N].", "y", "n");
            } while (continueEditing == "Y");
        }
        public void EditName()
        {
            string newName;
            Console.WriteLine("\n-----Name Edit-----");
            while (true)
            {
                Console.WriteLine("Enter new name: ");
                newName = Console.ReadLine();
                string reply;
                reply = CustomerAccount.ValidateName(newName);
                if (reply.Length == 0)
                    break;
                Console.WriteLine("Invalid name: " + reply);
            }
            account.SetName(newName);
        }
        public void PayInFunds()
        {
            decimal depositValue;
            Console.WriteLine("\n-----Deposit Funds-----");
            while (true)
            {
                depositValue = CustomerAccount.ValidateDecimal("Enter how much money you would like to deposit: ", 0, 10000);
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
                withdrawValue = CustomerAccount.ValidateDecimal("Enter how much money you would like to withdraw: ", 0, 1000);
                if (account.WithdrawFunds(withdrawValue))
                    break;
            }

        }
        public static string ThisOrThat(string prompt, string errorMsg, string answerOne, string answerTwo)
        {
            if (answerOne == answerTwo)
            {
                //throw an exception that says your two answers are the same
            }
            string answer;
            while (true)
            {
                Console.WriteLine(prompt);
                answer = AccountEditUI.TrimLower(Console.ReadLine());
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
    public class BankException : Exception
    {
        public BankException(String message) :
            base(message)
        {
        }
    }
    class BankProgram
    {

        public static void Main()
        {
            string input;
            Console.WriteLine("------Welcome to the bank------\n\n\n");
            DictionaryBank ourBank = DictionaryBank.Load("Test.txt");

            if (!ourBank.AccountsInBank()) //at the moment, if this tries to load an empty document, it returns a null value and throws an exception that i cant do anything with. Might need to try a "TRYCATCH"  
            {
                //
            }
            do
            {
                Console.WriteLine("Would you like to create a new account or edit an existing one?\n" +
                    "Type new to create an account or type edit to edit an existing account (to exit, type exit).");
                input = AccountEditUI.TrimLower(Console.ReadLine());
                switch (input)
                {
                    case "new":
                        AccountCreateUI.CreateScript(ourBank);
                        break;
                    case "edit":
                        AccountEditUI.EditScript(ourBank);
                        break;

                }
            } while (input != "exit");


            //CustomerAccount newAccount = new CustomerAccount("Rob", 10000);

            //if (ourBank.StoreAccount(newAccount))
            //    Console.WriteLine("CustomerAccount added to bank with account number: " + newAccount.GetAccountNumber());

            //BabyAccount newBabyAccount = new BabyAccount("David", 100, "Rob");

            //if (ourBank.StoreAccount(newBabyAccount))
            //    Console.WriteLine("BabyAccount added to bank with account number: " + newBabyAccount.GetAccountNumber());
            //Console.WriteLine();

            ourBank.Save("Test.txt");


            //DictionaryBank loadBank = DictionaryBank.Load("Test.txt");

            //IAccount storedAccount = loadBank.FindAccount("David");
            //if (storedAccount != null)
            //{
            //    Console.WriteLine(storedAccount.GetType() + " found in bank");
            //}
            //storedAccount = loadBank.FindAccount("Jack");
            //if (storedAccount != null)
            //{
            //    Console.WriteLine(storedAccount.GetType() + " found in bank");
            //}

            Console.ReadLine();
        }
    }
}

