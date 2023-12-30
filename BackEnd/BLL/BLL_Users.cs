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

        public User Delete_User(int id)
        {
            return _DAL_Users.Delete_User(id);
        }

        public bool Update_User(User2 model)
        {
            return _DAL_Users.Update_User(model);
        }
        public bool Update_User_ALL(User model)
        {
            return _DAL_Users.Update_User_ALL(model);
        }
        public bool Create_User(User model)
        {
            return _DAL_Users.Create_User(model);
        }
        public bool Deletes_User(List_User model)
        {
            return _DAL_Users.Deletes_User(model);
        }


        public List<User> Search_User(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Users.Search_User(pageIndex, pageSize, out total, Keywords);
        }
    }
}
