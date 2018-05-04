using System;

namespace AccountManagement

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
}
