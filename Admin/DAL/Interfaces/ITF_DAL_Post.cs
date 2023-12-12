using DTO;
using static DTO.Post;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Post
    {
        Post getpost(int id);
        bool Create_Post(Post model);
        bool Update_Post(Post model);
        Post Delete_Post(int id);

        bool Deletes_Post(LIST_Post model);

        List<Post> Search_Posts(int pageIndex, int pageSize, out long total, string Keywords);

    }
}
