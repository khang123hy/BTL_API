using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Notification : ControllerBase
    {
        ITF_BLL_Notification _notification;
        public User_API_Notification(ITF_BLL_Notification notification)
        {
            _notification = notification;
        }

        [Route("Search-Notification")]
        [HttpPost]
        public IActionResult Search_Notification_User([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"])))
                {
                    Keywords = Convert.ToString(formData["Keywords"]);
                }

                long total = 0;
                var data = _notification.Search_Notification_User(out total, Keywords);

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
