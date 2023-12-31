using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Comment
    {
        Comment getComment_byid(int id);
        List<Comment> get_comment_byid_post(int id);
        Comment Delete_Comment(int id);

        bool Update_Comment(Comment model);

        bool Create_Comment(Comment model);
        bool Create_Comment_Notification(CommentAndNotfication model);
        Comment Delete_Comment_Notification(int id);

        bool Deletes_Notification(LIST_Comment model);

        List<Comment> Search_Comment(int pageIndex, int pageSize, out long total, string Keywords);
        public List<Comment2> Search_Comment_User(int pageIndex, int pageSize, out long total, string Keywords);

    }
}
