using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_Like : ControllerBase
    {
        private ITF_BLL_Like _BLL_Like;
        public Admin_API_Like(ITF_BLL_Like bLL_Like)
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
        public Like Getlikeby_User(int id_user)
        {
            return _BLL_Like.Getlikeby_User(id_user);
        }

        [Route("Delete-like")]
        [HttpDelete]
        public bool Delete_Like(Like model)
        {
            return _BLL_Like.Delete_Like(model);
        }

        [Route("Create-like")]
        [HttpPost]
        public bool Create_Like(Like model)
        {
            return _BLL_Like.Create_Like(model);
        }

    }
}
