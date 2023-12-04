using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Users
    {

        User GetUser_byID(int id);

        bool Create_User(User model);
        bool Update_User(User model);

    }
}
