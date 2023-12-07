using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Users
    {

        User GetUser_byID(int id);

        User Delete_User(int id);

        bool Update_User(User model);
        bool Create_User(User model);

    }
}
