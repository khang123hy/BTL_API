using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Users : ITF_BLL_Users
    {
        private ITF_DAL_Users _DAL_Users;
        public BLL_Users(ITF_DAL_Users dal_Users)
        {
            _DAL_Users = dal_Users;
        }

        public User GetUser_byID(int id)
        {
            return _DAL_Users.GetUser_byID(id);
        }

        public bool Create_User(User model)
        {
            return _DAL_Users.Create_User(model);
        }

        public bool Update_User(User model)
        {
            return _DAL_Users.Update_User(model);
        }
    }
}
