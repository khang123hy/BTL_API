using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using static DTO.Post;

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

        [Route("Get-post")]
        [HttpGet]
        public Post getpost(int id)
        {
            return _Post.getpost(id);
        }


        [Route("Create-Post")]
        [HttpPost]
        public Post CreatePost([FromBody] Post model)
        {
            _Post.Create_Post(model);
            return model;
        }

        [Route("Update-Post")]
        [HttpPost]
        public Post UpdatePost([FromBody] Post model)
        {
            _Post.Update_Post(model);
            return model;
        }

        [Route("Delete-Post")]
        [HttpDelete]
        public Post Delete_Post(int id)
        {
            return _Post.Delete_Post(id);
        }

        [Route("Deletes-Post")]
        [HttpDelete]
        public LIST_Post Deletes_Post([FromBody] LIST_Post model)
        {
            _Post.Deletes_Post(model);
            return model;
        }
    }
}
