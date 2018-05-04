using System;
using System.Collections.Generic;
using AccountManagement;

namespace Dictionary
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
        public void DeleteAccount(int accountNumber)
        {
            try
            {
                accountDictionary.Remove(accountNumber);
            }
            catch
            {
                Console.WriteLine("Something went wrong. Whoops.");
            }
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
}
