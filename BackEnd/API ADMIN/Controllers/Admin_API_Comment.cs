using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_Comment : ControllerBase
    {
        private ITF_BLL_Comment _Comment;
        private ITF_BLL_File _File;
        public Admin_API_Comment(ITF_BLL_Comment comment, ITF_BLL_File file)
        {
            _Comment = comment;
            _File = file;
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

        [Route("Search-Comment")]
        [HttpPost]
        public IActionResult Search_Comment([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _Comment.Search_Comment(page, pageSize, out total, Keywords);
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

        [Route("Upload-Comments")]
        [HttpPut]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"Comments/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _File.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể upload tệp");
            }
        }
    }
}
