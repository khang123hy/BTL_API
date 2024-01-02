using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Notification
    {

        Notification GetDatabyID(int id);
        bool Create(Notification model);
        bool Update(Notification model);
        Notification Delete(int id);
        bool Deletes_Notification(LIST_Notification model);
        List<Notification> Search_Notification(int pageIndex, int pageSize, out long total, string Keywords);
        List<Notification2> Search_Notification_User(out long total, string Keywords);
    }
}
