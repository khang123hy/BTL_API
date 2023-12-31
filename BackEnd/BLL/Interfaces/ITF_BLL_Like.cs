using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Like
    {
        Like Getlikeby_Posts(int id_post);
        Like Getlikeby_User(int id_user, int id_post);

        Like Delete_Like(int id);
        bool Create_Like(Like model);
        bool Create_Like_Notification(LikeAndNotification model);
        List<Like2> Search_Like_Posts(out long total, string Keywords);
    }
}
