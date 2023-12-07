using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Account
    {
        LoginResult Login(string taikhoan, string matkhau);

    }
}
