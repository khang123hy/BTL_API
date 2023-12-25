using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using static DTO.Notification;

namespace API.Controllers
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

        [Route("get-by-id")]
        [HttpGet]
        public Notification GetDatabyID(int id)
        {
            return _notification.GetDatabyID(id);
        }

        [Route("Create-Notification")]
        [HttpPost]
        public bool Create(Notification model)
        {
            return _notification.Create(model);
        }

        [Route("Update-Notification")]
        [HttpPost]
        public bool Update(Notification model)
        {
            return _notification.Update(model);
        }


        [Route("Delete-Notification")]
        [HttpDelete]
        public Notification Delete(int id)
        {
            return _notification.Delete(id);
        }

        [Route("Deletes-Notification")]
        [HttpDelete]
        public bool Deletes_Notification(LIST_Notification model)
        {
            return _notification.Deletes_Notification(model);
        }

        [Route("Search-Posts")]
        [HttpPost]
        public IActionResult Search_Notification([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _notification.Search_Notification(page, pageSize, out total, Keywords);
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
