using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_Users : ControllerBase
    {
        private ITF_BLL_Users _BLL_Users;
        private ITF_BLL_File _BLL_File;
        public Admin_API_Users(ITF_BLL_Users bll_users, ITF_BLL_File bLL_File)
        {
            _BLL_Users = bll_users;
            _BLL_File = bLL_File;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public User GetUser_byID(int id)
        {
            return _BLL_Users.GetUser_byID(id);
        }
        [Route("Delete-User")]
        [HttpDelete]
        public User Delete_User(int id)
        {
            return _BLL_Users.Delete_User(id);
        }

        [Route("Update-User")]
        [HttpPost]
        public bool Update_User(User2 model)
        {
            return _BLL_Users.Update_User(model);
        }
        [Route("Update-User-all")]
        [HttpPost]
        public bool Update_User_ALL(User model)
        {
            return _BLL_Users.Update_User_ALL(model);
        }
        [Route("Create-User")]
        [HttpPost]
        public bool Create_User(User model)
        {
            return _BLL_Users.Create_User(model);
        }

        [Route("Deletes-User")]
        [HttpDelete]
        public List_User Deletes_User([FromBody] List_User model)
        {
            _BLL_Users.Deletes_User(model);
            return model;
        }

        [Route("Search-User")]
        [HttpPost]
        public IActionResult Search_User([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _BLL_Users.Search_User(page, pageSize, out total, Keywords);
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

        [Route("Upload-User")]
        [HttpPut]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"USERS/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _BLL_File.CreatePathFile(filePath);
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
