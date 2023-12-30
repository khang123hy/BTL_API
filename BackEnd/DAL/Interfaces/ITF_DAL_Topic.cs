using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Topic
    {
        Topic GetTopicbyID(int id);
        Topic Delete(int id);
        bool Create(Topic model);
        bool Update(Topic model);

        bool Deletes_Topic(LIST_Topic model);

        List<Topic> Search_Topic(int pageIndex, int pageSize, out long total, string Keywords);

    }
}
