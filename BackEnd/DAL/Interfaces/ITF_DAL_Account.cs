using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Account
    {



        //Account

        Check check_AccountName(string AccountName, string Email);

        bool Create_Account(Account model);

    }
}
