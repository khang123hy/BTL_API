using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Notification
    {

        Notification GetDatabyID(int id);
        bool Create(Notification model);
        bool Update(Notification model);
    }
}
