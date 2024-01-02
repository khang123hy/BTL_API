using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Account : ITF_BLL_Account
    {
        private ITF_DAL_Account _DAL_Account;

        public BLL_Account(ITF_DAL_Account dAL_Account)
        {
            _DAL_Account = dAL_Account;
        }

        public Check check_AccountName(string AccountName, string Email)
        {
            return _DAL_Account.check_AccountName(AccountName, Email);
        }

        public bool Create_Account(Account model)
        {
            return _DAL_Account.Create_Account(model);
        }


    }
}
