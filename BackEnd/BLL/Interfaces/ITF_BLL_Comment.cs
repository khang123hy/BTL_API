using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Comment
    {
        public Comment getComment_byid(int id);

        List<Comment> get_comment_byid_post(int id);

        public Comment Delete_Comment(int id);

        Comment Delete_Comment_Notification(int id);

        public bool Update_Comment(Comment model);

        public bool Create_Comment(Comment model);
        bool Create_Comment_Notification(CommentAndNotfication model);

        bool Deletes_Notification(LIST_Comment model);
        List<Comment> Search_Comment(int pageIndex, int pageSize, out long total, string Keywords);

        public List<Comment2> Search_Comment_User(int pageIndex, int pageSize, out long total, string Keywords);


    }
}
