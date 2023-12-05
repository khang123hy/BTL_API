using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Topic
    {
        Topic GetTopicbyID(int id);
        bool Create_Topic(Topic model);
        bool Update_Topic(Topic model);
    }
}
