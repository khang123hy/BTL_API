using DTO;

namespace BLL.Interfaces
{

    public partial interface ITF_BLL_Users
    {
        User GetUser_byID(int id);
        User Delete_User(int id);
        bool Update_User(User2 model);
        bool Update_User_ALL(User model);
        bool Create_User(User model);
        bool Deletes_User(List_User model);

        List<User> Search_User(int pageIndex, int pageSize, out long total, string Keywords);
    }


}
