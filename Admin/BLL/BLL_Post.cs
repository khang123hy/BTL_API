using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Post : ITF_BLL_Post
    {
        private ITF_DAL_Post _DAL_Post;
        public BLL_Post(ITF_DAL_Post dAL_Post)
        {
            _DAL_Post = dAL_Post;
        }
        public Post getpost(int id)
        {
            return _DAL_Post.getpost(id);
        }
        public bool Update_Post(Post model)
        {
            return _DAL_Post.Update_Post(model);
        }
        public bool Create_Post(Post model)
        {
            return _DAL_Post.Create_Post(model);
        }
    }
}
