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

        //public LoginResult Login(string taikhoan, string matkhau)
        //{
        //    var account = _DAL_Account.Login(taikhoan, matkhau);
        //    if (account == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        //    // Kiểm tra giá trị trước khi gán Token
        //    if (account != null)
        //    {
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //        new Claim(ClaimTypes.Name, account.FullName),
        //        new Claim(ClaimTypes.Role, account.Role),
        //        new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup, account.Password)
        //            }),

        //            Expires = DateTime.UtcNow.AddDays(7),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        account.Token = tokenHandler.WriteToken(token);
        //    }

        //    return account;
        //}


        //Account

        public bool Update_Account(Account model)
        {
            return _DAL_Account.Update_Account(model);
        }
        public bool Deletes_Account(List_User model)
        {
            return _DAL_Account.Deletes_Account(model);
        }
        public Account Delete_Account(int id)
        {
            return _DAL_Account.Delete_Account(id);
        }

        public List<Account> Search_Account(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Account.Search_Account(pageIndex, pageSize, out total, Keywords);
        }
    }
}
