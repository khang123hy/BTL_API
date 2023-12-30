using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Topic : ControllerBase
    {

        private ITF_BLL_Topic _topic;
        public User_API_Topic(ITF_BLL_Topic topic)
        {
            _topic = topic;
        }

        [Route("get-Topic-by-id/{id}")]
        [HttpGet]
        public Topic GetTopicbyid(int id)
        {
            return _topic.GetTopicbyID(id);
        }

        [Route("Search-Topic")]
        [HttpPost]
        public IActionResult Search_Topic([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _topic.Search_Topic(page, pageSize, out total, Keywords);
                return Ok(
                            new
                            {
                                TotalItems = total,
                                Data = data,
                                Page = page,
                                PageSize = pageSize
                            }
                        );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

