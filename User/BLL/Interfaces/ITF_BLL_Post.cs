using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Post
    {
        public Post getpost(int id);
        Post3 getpost_by_id_User(int id);

        bool Create_Post(Post post);
        bool Update_Post(Post post);
        Post Delete_Post(int id);

        bool Deletes_Post(LIST_Post model);

        List<Post> Search_Posts(int pageIndex, int pageSize, out long total, string Keywords);
        List<Post2> Search_Posts_User(int pageIndex, int pageSize, out long total, string Keywords);

    }
}
