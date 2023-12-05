using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Topic : ITF_BLL_Topic
    {
        public ITF_DAL_Topic _DAL_Topic;
        public BLL_Topic(ITF_DAL_Topic dAL_Topic)
        {
            _DAL_Topic = dAL_Topic;
        }

        public Topic GetTopicbyID(int id)
        {
            return _DAL_Topic.GetTopicbyID(id);
        }

        public bool Create_Topic(Topic model)
        {
            return _DAL_Topic.Create_Topic(model);
        }
        public bool Update_Topic(Topic model)
        {
            return _DAL_Topic.Update_Topic(model);
        }
    }
}
