using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Comment : ControllerBase
    {
        private ITF_BLL_Comment _Comment;
        public API_Comment(ITF_BLL_Comment comment)
        {
            _Comment = comment;
        }
        [Route("get-comment-byid")]
        [HttpGet]


        public Comment getComment_byid(int id)
        {
            return _Comment.getComment_byid(id);
        }
    }
}
