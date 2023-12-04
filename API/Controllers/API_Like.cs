using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Like : ControllerBase
    {
        private ITF_BLL_Like _BLL_Like;
        public API_Like(ITF_BLL_Like bLL_Like)
        {
            _BLL_Like = bLL_Like;
        }

        [Route("get-like-byid/{id}")]
        [HttpGet]

        public Like GetlikebyID(int id)
        {
            return _BLL_Like.GetlikebyID(id);
        }
    }
}
