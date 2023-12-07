using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Notification
    {
        Notification GetDatabyID(int id);
        bool Create(Notification model);
        bool Update(Notification model);
    }
}
