using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Users
    {

        //User
        User GetUser_byID(int id);

        User Delete_User(int id);

        bool Update_User(User model);
        bool Create_User(User2 model);
        bool Deletes_User(List_User model);

        List<User> Search_User(int pageIndex, int pageSize, out long total, string Keywords);

    }
}
