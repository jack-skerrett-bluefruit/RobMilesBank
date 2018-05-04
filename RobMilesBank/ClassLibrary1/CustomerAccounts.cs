using System;
using System.Collections.Generic;


namespace AccountManagement

{
    public class CustomerAccount : IAccount
    {
        public CustomerAccount(
            string newName,
            decimal initialBalance)
        {
            name = newName;
            balance = initialBalance;
        }
        private decimal balance = 0;
        private string name;
        private string accountNumber = UniqueAccountNumber();
        private static List<int> accountNumbers = new List<int>();
        public virtual bool WithdrawFunds(decimal amount)
        {
            if (balance < amount)
            {
                return false;
            }
            balance = balance - amount;
            return true;
        }
        public void PayInFunds(decimal amount)
        {
            balance += amount;
        }
        public decimal GetBalance()
        {
            return balance;
        }
        public string GetName()
        {
            return name;
        }
        public string GetAccountNumber()
        {
            return accountNumber;
        }
        public bool SetName(string inName)
        {
            this.name = inName.Trim();
            return true;
        }
        public static string UniqueAccountNumber()
        {
            Random rdn = new Random();
            string accountNumber;
            int intAccountNumber;
            while (true)
            {
                accountNumber = rdn.Next(0, 1000000).ToString("000000");
                intAccountNumber = int.Parse(accountNumber);
                if (accountNumbers.Contains(intAccountNumber))
                {
                    continue;
                }
                accountNumbers.Add(intAccountNumber);
                return accountNumber;
            }

        }
        public virtual void Save(System.IO.TextWriter textOut)
        {
            textOut.WriteLine(accountNumber);
            textOut.WriteLine(name);
            textOut.WriteLine(balance);

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
        public static CustomerAccount Load(
            System.IO.TextReader textIn)
        {
            CustomerAccount result = null;
            try
            {
                int accountNumber = int.Parse(textIn.ReadLine());
                string name = textIn.ReadLine();
                string balanceText = textIn.ReadLine();
                decimal balance = decimal.Parse(balanceText);
                result = new CustomerAccount(name, balance);
            }
            catch
            {
                return null;
            }
            return result;
        }
        public static CustomerAccount Load(string filename)
        {
            System.IO.TextReader textIn = null;
            CustomerAccount result = null;
            try
            {
                textIn = new System.IO.StreamReader(filename);
                result = CustomerAccount.Load(textIn);
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
        public CustomerAccount(System.IO.TextReader textIn)
        {
            accountNumber = textIn.ReadLine();
            name = textIn.ReadLine();
            string balanceText = textIn.ReadLine();
            balance = decimal.Parse(balanceText);
        }
        public override string ToString()
        {
            return "\nThe account number is " + accountNumber +
                "\nThe account holders name is " + name +
                "\nThe balance of this account is " + balance;
        }
    }
}
