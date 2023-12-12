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


        //Account
        bool Update_Account(Account model);
        bool Deletes_Account(List_User model);
        Account Delete_Account(int id);
    }
}
