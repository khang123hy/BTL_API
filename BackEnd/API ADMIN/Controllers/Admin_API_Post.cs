using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_Post : ControllerBase
    {

        private ITF_BLL_Post _Post;
        private ITF_BLL_File _PostFile;
        public Admin_API_Post(ITF_BLL_Post post, ITF_BLL_File postFile)
        {
            _Post = post;
            _PostFile = postFile;
        }

        [Route("Get-posts")]
        [HttpGet]
        public Post getpost(int id)
        {
            return _Post.getpost(id);
        }


        [Route("Create-Posts")]
        [HttpPost]
        public Post CreatePost([FromBody] Post model)
        {
            _Post.Create_Post(model);
            return model;
        }

        [Route("Update-Posts")]
        [HttpPost]
        public Post UpdatePost([FromBody] Post model)
        {
            _Post.Update_Post(model);
            return model;
        }

        [Route("Delete-Posts")]
        [HttpDelete]
        public Post Delete_Post(int id)
        {
            return _Post.Delete_Post(id);
        }

        [Route("Deletes-Posts")]
        [HttpDelete]
        public LIST_Post Deletes_Post([FromBody] LIST_Post model)
        {
            _Post.Deletes_Post(model);
            return model;
        }

        [Route("Search-Posts")]
        [HttpPost]
        public IActionResult Search_Posts([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _Post.Search_Posts(page, pageSize, out total, Keywords);
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

        [Route("Upload-Posts")]
        [HttpPut]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"POSTS/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _PostFile.CreatePathFile(filePath);
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
