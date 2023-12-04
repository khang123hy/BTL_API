using DTO;

namespace BLL.Interfaces
{
    public partial interface ITF_BLL_Topic
    {
        Topic GetTopicbyID(int id);
        bool Create_Topic(Topic topic);
        bool Update_Topic(Topic topic);
    }
}
