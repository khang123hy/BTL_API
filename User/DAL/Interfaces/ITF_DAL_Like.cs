using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Like
    {
        Like Getlikeby_Posts(int id_post);
        Like Getlikeby_User(int id_user);
        bool Delete_Like(Like model);
        bool Create_Like(Like model);
    }
}
