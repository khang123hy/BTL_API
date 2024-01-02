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
        private ITF_BLL_File _File;
        public User_API_Post(ITF_BLL_Post post, ITF_BLL_File file)
        {
            _Post = post;
            _File = file;
        }


        [Route("Get-posts-user")]
        [HttpGet]
        public Post3 getpost_by_id_User(int id)
        {
            return _Post.getpost_by_id_User(id);
        }

        [Route("Search-PostDeteails")]
        [HttpPost]
        public IActionResult Search_PostDetails_User([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"])))
                {
                    Keywords = Convert.ToString(formData["Keywords"]);
                }

                long total = 0;
                var data = _Post.Search_PostDetails_User(out total, Keywords);

                return Ok(
                    new
                    {
                        TotalItems = total,
                        Data = data
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }





        [Route("Create-Posts_list")]
        [HttpPost]
        public Post_list Create_post_list([FromBody] Post_list model)
        {
            _Post.Create_post_list(model);
            return model;
        }

        [Route("Search-Postsc-User_Asc")]
        [HttpPost]
        public IActionResult Search_Posts_User_Asc([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                string OrderBy = "";
                if (formData.Keys.Contains("OrderBy") && !string.IsNullOrEmpty(Convert.ToString(formData["OrderBy"]))) { OrderBy = Convert.ToString(formData["OrderBy"]); }
                long total = 0;
                var data = _Post.Search_Posts_User_Asc(page, pageSize, out total, Keywords, OrderBy);
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
        [Route("Search-Postsc-User_Desc")]
        [HttpPost]
        public IActionResult Search_Posts_User_Desc([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                string OrderBy = "";
                if (formData.Keys.Contains("OrderBy") && !string.IsNullOrEmpty(Convert.ToString(formData["OrderBy"]))) { OrderBy = Convert.ToString(formData["OrderBy"]); }
                long total = 0;
                var data = _Post.Search_Posts_User_Desc(page, pageSize, out total, Keywords, OrderBy);
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

        [Route("Search-Posts-by-Topic-User_Asc")]
        [HttpPost]
        public IActionResult Search_Posts_by_Topic_User_Asc([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                string OrderBy = "";
                if (formData.Keys.Contains("OrderBy") && !string.IsNullOrEmpty(Convert.ToString(formData["OrderBy"]))) { OrderBy = Convert.ToString(formData["OrderBy"]); }
                string ID_Topic = "";
                if (formData.Keys.Contains("ID_Topic") && !string.IsNullOrEmpty(Convert.ToString(formData["ID_Topic"]))) { ID_Topic = Convert.ToString(formData["ID_Topic"]); }
                long total = 0;
                var data = _Post.Search_Posts_by_Topic_User_Asc(page, pageSize, out total, Keywords, OrderBy, ID_Topic);
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

        [Route("Search-Posts-by-Topic-User_Desc")]
        [HttpPost]
        public IActionResult Search_Posts_by_Topic_User_Desc([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                string OrderBy = "";
                if (formData.Keys.Contains("OrderBy") && !string.IsNullOrEmpty(Convert.ToString(formData["OrderBy"]))) { OrderBy = Convert.ToString(formData["OrderBy"]); }
                string ID_Topic = "";
                if (formData.Keys.Contains("ID_Topic") && !string.IsNullOrEmpty(Convert.ToString(formData["ID_Topic"]))) { ID_Topic = Convert.ToString(formData["ID_Topic"]); }
                long total = 0;
                var data = _Post.Search_Posts_by_Topic_User_Desc(page, pageSize, out total, Keywords, OrderBy, ID_Topic);
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


        [Route("Upload-Post_User")]
        [HttpPut]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"Posts/{file.FileName.Replace("-", "_").Replace("%", "")}";
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
