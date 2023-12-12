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

        public Topic Delete(int id)
        {
            return _DAL_Topic.Delete(id);
        }

        public bool Create(Topic model)
        {
            return _DAL_Topic.Create(model);
        }

        public bool Update(Topic model)
        {
            return _DAL_Topic.Update(model);
        }

        public bool Deletes_Topic(LIST_Topic model)
        {
            return _DAL_Topic.Deletes_Topic(model);
        }

        public List<Topic> Search_Topic(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Topic.Search_Topic(pageIndex, pageSize, out total, Keywords);
        }
    }
}
