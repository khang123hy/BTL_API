using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using static DTO.Notification;

namespace BLL
{
    public class BLL_Notification : ITF_BLL_Notification
    {
        ITF_DAL_Notification _dal_notification;
        public BLL_Notification(ITF_DAL_Notification dal_notification)
        {
            _dal_notification = dal_notification;
        }

        public Notification GetDatabyID(int id)
        {
            return _dal_notification.GetDatabyID(id);
        }

        public bool Create(Notification model)
        {
            return _dal_notification.Create(model);
        }

        public bool Update(Notification model)
        {
            return _dal_notification.Update(model);
        }

        public Notification Delete(int id)
        {
            return _dal_notification.Delete(id);
        }

        public bool Deletes_Notification(LIST_Notification model)
        {
            return _dal_notification.Deletes_Notification(model);
        }
    }
}
