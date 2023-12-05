using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Post : ControllerBase
    {

        private ITF_BLL_Post _Post;
        public API_Post(ITF_BLL_Post post)
        {
            _Post = post;
        }

        [Route("get-post")]
        [HttpGet]
        public Post getpost(int id)
        {
            return _Post.getpost(id);
        }


        [Route("create-Post")]
        [HttpPost]
        public Post CreatePost([FromBody] Post model)
        {
            _Post.Create_Post(model);
            return model;
        }

        [Route("update-Post")]
        [HttpPost]
        public Post UpdatePost([FromBody] Post model)
        {
            _Post.Update_Post(model);
            return model;
        }
    }
}
