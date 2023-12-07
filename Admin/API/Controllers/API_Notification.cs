using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Notification : ControllerBase
    {
        ITF_BLL_Notification _notification;
        public API_Notification(ITF_BLL_Notification notification)
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
    }
}
