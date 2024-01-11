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

        [Route("Create-Posts_list")]
        [HttpPost]
        public Post_list Create_post_list([FromBody] Post_list model)
        {
            _Post.Create_post_list(model);
            return model;
        }

        [Route("Update-Posts_list")]
        [HttpPost]
        public Post_list Update_post_list([FromBody] Post_list model)
        {
            _Post.Update_post_list(model);
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

        [Route("Search-Posts-List")]
        [HttpPost]
        public IActionResult Search_Posts_Admin([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _Post.Search_Posts_Admin(page, pageSize, out total, Keywords);
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
                // Kiểm tra xem tệp tin có dung lượng lớn hơn 0 hay không
                if (file.Length > 0)
                {
                    // Tạo đường dẫn tệp tin trong thư mục "POSTS"
                    string filePath = $"POSTS/{file.FileName.Replace("-", "_").Replace("%", "")}";

                    // Tạo đường dẫn tuyệt đối và kiểm tra/thêm thư mục nếu cần
                    var fullPath = _PostFile.CreatePathFile(filePath);

                    // Tạo FileStream để ghi dữ liệu tệp tin lên đĩa
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        // Sao chép dữ liệu từ tệp tin đã được gửi lên vào FileStream
                        await file.CopyToAsync(fileStream);
                    }

                    // Trả về đường dẫn của tệp tin đã tải lên với mã trạng thái HTTP 200 (OK)
                    return Ok(new { filePath });
                }
                else
                {
                    // Trả về BadRequest nếu không có tệp tin được gửi lên
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về mã trạng thái HTTP 500 (Internal Server Error) và thông báo lỗi
                return StatusCode(500, "Không thể upload tệp: " + ex.Message);
            }
        }

    }
}
