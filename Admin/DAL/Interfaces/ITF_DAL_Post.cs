using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Post
    {
        Post getpost(int id);
        bool Create_Post(Post model);
        bool Update_Post(Post model);
    }
}
