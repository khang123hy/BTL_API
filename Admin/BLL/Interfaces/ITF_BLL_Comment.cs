using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Comment
    {
        public Comment getComment_byid(int id);
        public Comment Delete_Comment(int id);

        public bool Update_Comment(Comment model);

        public bool Create_Comment(Comment model);

        bool Deletes_Notification(LIST_Comment model);

    }
}
