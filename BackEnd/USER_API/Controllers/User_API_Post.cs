using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Post : ControllerBase
    {

        private ITF_BLL_Post _Post;
        public User_API_Post(ITF_BLL_Post post)
        {
            _Post = post;
        }

        [Route("Get-posts")]
        [HttpGet]
        public Post getpost(int id)
        {
            return _Post.getpost(id);
        }

        [Route("Get-posts-user")]
        [HttpGet]
        public Post3 getpost_by_id_User(int id)
        {
            return _Post.getpost_by_id_User(id);
        }
        [Route("Create-Posts")]
        [HttpPost]
        public Post CreatePost([FromBody] Post model)
        {
            _Post.Create_Post(model);
            return model;
        }




        [Route("Search-Posts-User")]
        [HttpPost]
        public IActionResult Search_Posts_User([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _Post.Search_Posts_User(page, pageSize, out total, Keywords);
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

        [Route("Search-Posts-by-Topic-User")]
        [HttpPost]
        public IActionResult Search_Posts_by_Topic_User([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _Post.Search_Posts_by_Topic_User(page, pageSize, out total, Keywords);
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
