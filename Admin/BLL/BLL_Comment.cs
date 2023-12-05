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
    }
}
