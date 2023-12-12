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

        [Route("Delete-Comment")]
        [HttpDelete]
        public Comment Delete_Comment(int id)
        {
            return _Comment.Delete_Comment(id);
        }

        [Route("Update-Comment")]
        [HttpPost]
        public bool Update_Comment(Comment model)
        {
            return _Comment.Update_Comment(model);
        }

        [Route("Create-Comment")]
        [HttpPost]
        public bool Create_Comment(Comment model)
        {
            return _Comment.Create_Comment(model);
        }

        [Route("Deletes-Comment")]
        [HttpDelete]
        public bool Deletes_Notification(LIST_Comment model)
        {
            return _Comment.Deletes_Notification(model);
        }
    }
}
