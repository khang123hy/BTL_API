using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Like : ITF_BLL_Like
    {
        public ITF_DAL_Like _DAL_Like;
        public BLL_Like(ITF_DAL_Like dAL_Like)
        {
            _DAL_Like = dAL_Like;
        }


        public Like Getlikeby_Posts(int id_post)
        {
            return _DAL_Like.Getlikeby_Posts(id_post);
        }


        public Like Getlikeby_User(int id_user, int id_post)
        {
            return _DAL_Like.Getlikeby_User(id_user, id_post);
        }

        public Like Delete_Like(int id)
        {
            return _DAL_Like.Delete_Like(id);
        }

        public bool Create_Like_Notification(LikeAndNotification model)
        {
            return _DAL_Like.Create_Like_Notification(model);
        }
        public bool Create_Like(Like model)
        {
            return _DAL_Like.Create_Like(model);
        }

        public List<Like2> Search_Like_Posts(out long total, string Keywords)
        {
            return _DAL_Like.Search_Like_Posts(out total, Keywords);
        }
    }
}
