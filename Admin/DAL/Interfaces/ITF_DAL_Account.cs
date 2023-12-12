using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Account
    {

        //LoginResult Login(string taikhoan, string matkhau);

        //Account
        bool Update_Account(Account model);
        bool Deletes_Account(List_User model);
        Account Delete_Account(int id);

        List<Account> Search_Account(int pageIndex, int pageSize, out long total, string Keywords);
    }
}
