using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBanking
{
    public interface IAccount
    {
        void PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        decimal GetBalance();
        string GetName();
        int GetAccountNumber();
        bool SetName(string inName);
        bool Save(string filename);
        void Save(System.IO.TextWriter textOut);
        //bool SetParentName(string inParentName);
    }
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
        private int accountNumber = UniqueAccountNumber();
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
        public int GetAccountNumber()
        {
            return accountNumber;
        }
        public bool SetName(string inName)
        {
            this.name = inName.Trim();
            return true;
        }
        public static int UniqueAccountNumber()
        {
            Random rdn = new Random();
            int accountNumber;
            while (true)
            {
                accountNumber = rdn.Next(100000, 1000000);
                if (accountNumbers.Contains(accountNumber))
                {
                    continue;
                }
                accountNumbers.Add(accountNumber);
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
            accountNumber = int.Parse(textIn.ReadLine());
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
    public class BabyAccount : CustomerAccount
    {
        private string parentName;
        private int maxWithdrawal = 10;
        public string GetParentName()
        {
            return parentName;
        }
        //public bool SetParentName(string inParentName)
        //{
        //    parentName = inParentName.Trim();
        //    return true;
        //}
        public override bool WithdrawFunds(decimal amount)
        {
            if (amount > maxWithdrawal)
            {
                Console.WriteLine("There is a maximum of {0:C} on baby accounts.", maxWithdrawal);
                return false;
            }
            return base.WithdrawFunds(amount);
        }

        public override void Save(System.IO.TextWriter textOut)
        {
            base.Save(textOut);
            textOut.WriteLine(parentName);
        }

        public BabyAccount(
            string newName,
            decimal initialBalance,
            string inParentName)
            : base(newName, initialBalance)
        {
            parentName = inParentName;
        }
        public BabyAccount(System.IO.TextReader textIn) :
            base(textIn)
        {
            parentName = textIn.ReadLine();
        }
        public override string ToString()
        {
            return base.ToString() +
                "\nThe parent name is " + parentName;
        }
    }
}
