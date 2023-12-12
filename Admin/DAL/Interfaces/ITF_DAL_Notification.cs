using DTO;
using static DTO.Notification;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Notification
    {

        Notification GetDatabyID(int id);
        bool Create(Notification model);
        bool Update(Notification model);
        Notification Delete(int id);

        bool Deletes_Notification(LIST_Notification model);
    }
}
