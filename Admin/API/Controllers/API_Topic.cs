using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Topic : ControllerBase
    {

        private ITF_BLL_Topic _topic;
        public API_Topic(ITF_BLL_Topic topic)
        {
            _topic = topic;
        }

        [Route("get-Tocpic-by-id/{id}")]
        [HttpGet]
        public Topic GetTopicbyid(int id)
        {
            return _topic.GetTopicbyID(id);
        }

        [Route("create-Topic")]
        [HttpPost]
        public bool Create(Topic model)
        {
            return _topic.Create(model);
        }


        [Route("update-Topic")]
        [HttpPost]
        public bool Update(Topic model)
        {
            return _topic.Update(model);
        }


        [Route("Topic-Delete/{id}")]
        [HttpDelete]
        public Topic Delete(int id)
        {
            return _topic.Delete(id);
        }
    }
}

