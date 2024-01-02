using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Account
    {
        Check check_AccountName(string AccountName, string Email);

        //Account
        bool Create_Account(Account model);


    }
}
