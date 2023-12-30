using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Options;

namespace BLL
{
    public class BLL_Account : ITF_BLL_Account
    {
        private ITF_DAL_Account _DAL_Account;
        private readonly AppSettings _appSettings;

        public BLL_Account(ITF_DAL_Account dAL_Account, IOptions<AppSettings> appSettingsn)
        {
            _DAL_Account = dAL_Account;
            _appSettings = appSettingsn.Value;
        }
        public bool Create_Account(Account model)
        {
            return _DAL_Account.Create_Account(model);
        }


    }
}
