using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Like : ControllerBase
    {
        private ITF_BLL_Like _BLL_Like;
        public User_API_Like(ITF_BLL_Like bLL_Like)
        {
            _BLL_Like = bLL_Like;
        }

        [Route("get-like-posts")]
        [HttpGet]
        public Like Getlikeby_Posts(int id_post)
        {
            return _BLL_Like.Getlikeby_Posts(id_post);
        }

        [Route("get-like-user")]
        [HttpGet]
        public Like Getlikeby_User(int id_user, int id_post)
        {
            return _BLL_Like.Getlikeby_User(id_user, id_post);
        }

        [Route("Delete-like")]
        [HttpDelete]
        public Like Delete_Like(int id)
        {
            return _BLL_Like.Delete_Like(id);
        }

        [Route("Create-like-Notification")]
        [HttpPost]
        public bool Create_Like_Notification(LikeAndNotification model)
        {
            return _BLL_Like.Create_Like_Notification(model);
        }

        [Route("Search-Like-Posts")]
        [HttpPost]
        public IActionResult Search_Like_Posts([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"])))
                {
                    Keywords = Convert.ToString(formData["Keywords"]);
                }

                long total = 0;
                var data = _BLL_Like.Search_Like_Posts(out total, Keywords);

                return Ok(
                    new
                    {
                        TotalItems = total,
                        Data = data
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
