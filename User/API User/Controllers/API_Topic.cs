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
        public Topic CreateTopic([FromBody] Topic model)
        {
            _topic.Create_Topic(model);
            return model;
        }

        [Route("update-Topic")]
        [HttpPost]
        public Topic UpdateTopic([FromBody] Topic model)
        {
            _topic.Update_Topic(model);
            return model;
        }
    }
}

