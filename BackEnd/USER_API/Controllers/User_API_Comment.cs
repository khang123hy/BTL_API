using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Comment : ControllerBase
    {
        private ITF_BLL_Comment _Comment;
        public User_API_Comment(ITF_BLL_Comment comment)
        {
            _Comment = comment;
        }




        [Route("Create-Comment")]
        [HttpPost]
        public bool Create_Comment(Comment model)
        {
            return _Comment.Create_Comment(model);
        }


        [Route("Search-Comment-User")]
        [HttpPost]
        public IActionResult Search_Comment_User([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _Comment.Search_Comment_User(page, pageSize, out total, Keywords);
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
