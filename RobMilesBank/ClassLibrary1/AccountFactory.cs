
namespace AccountManagement
{
    public class AccountFactory
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
}
