using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Comment
    {
        Comment getComment_byid(int id);
        Comment Delete_Comment(int id);

        bool Update_Comment(Comment model);

        bool Create_Comment(Comment model);

        bool Deletes_Notification(LIST_Comment model);


    }
}
