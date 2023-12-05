using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Post
    {
        public Post getpost(int id);
        bool Create_Post(Post post);
        bool Update_Post(Post post);
    }
}
