using System;
using Validators;
using AccountUI;
using Dictionary;


namespace RobMilesBank
{

    
    class BankProgram
    {
        public static void Main()
        {
            Console.WriteLine("\n-------------------------------\n" +
                "------Welcome to the bank------\n" +
                "-------------------------------\n\n\n");
            DictionaryBank ourBank = DictionaryBank.Load("database.txt");
            if (!ourBank.AccountsInBank()) //at the moment, if this tries to load an empty document, it returns a null value and throws an exception that i cant do anything with. Might need to try a "TRYCATCH"  
            {
               
            }

            string userValue;
            do
            {
                Console.WriteLine("\n-----Feature Select-----\n" +
                    "\n    Create a new account (type new)" +
                    "\n    Edit an existing account (type edit)" +
                    "\n    See the status of an existing account (type status)" +
                    "\n    Delete an existing account (type delete)" +
                    "\n    Or type exit to exit the program");
                userValue = Validation.TrimLower(Console.ReadLine());
                switch (userValue)
                {
                    case "new":
                        AccountCreateUI.CreateScript(ourBank);
                        break;
                    case "edit":
                        AccountEditUI.EditScript(ourBank);
                        break;
                    case "status":
                        AccountStatusUI.StatusScript(ourBank);
                        break;
                    case "delete":
                        AccountDeleteUI.DeleteScript(ourBank);
                        break;
                }
            } while (userValue != "exit");
            ourBank.Save("database.txt");
            Console.ReadLine();
        }
    }
}

