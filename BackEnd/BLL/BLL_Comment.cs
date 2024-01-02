using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Comment : ITF_BLL_Comment
    {
        public ITF_DAL_Comment _DAL_Comment;
        public BLL_Comment(ITF_DAL_Comment dAL_Comment)
        {
            _DAL_Comment = dAL_Comment;
        }

        public Comment getComment_byid(int id)
        {
            return _DAL_Comment.getComment_byid(id);
        }

        public List<Comment> get_comment_byid_post(int id)
        {
            return _DAL_Comment.get_comment_byid_post(id);
        }
        public Comment Delete_Comment_Notification(int id)
        {
            return _DAL_Comment.Delete_Comment_Notification(id);
        }
        public Comment Delete_Comment(int id)
        {
            return _DAL_Comment.Delete_Comment(id);
        }

        public bool Create_Comment(Comment model)
        {
            return _DAL_Comment.Create_Comment(model);
        }
        public bool Create_Comment_Notification(CommentAndNotfication model)
        {
            return _DAL_Comment.Create_Comment_Notification(model);
        }
        public bool Update_Comment(Comment model)
        {
            return _DAL_Comment.Update_Comment(model);
        }

        public bool Deletes_Notification(LIST_Comment model)
        {
            return _DAL_Comment.Deletes_Notification(model);
        }
        public List<Comment> Search_Comment(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Comment.Search_Comment(pageIndex, pageSize, out total, Keywords);
        }
        public List<Comment2> Search_Comment_User(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Comment.Search_Comment_User(pageIndex, pageSize, out total, Keywords);
        }
    }
}
