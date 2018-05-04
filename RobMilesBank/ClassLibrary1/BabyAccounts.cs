using System;

namespace AccountManagement
{
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
