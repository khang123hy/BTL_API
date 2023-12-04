using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Comment
    {
        Comment getComment_byid(int id);
    }
}
