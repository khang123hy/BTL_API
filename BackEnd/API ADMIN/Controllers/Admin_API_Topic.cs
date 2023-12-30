using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_Topic : ControllerBase
    {

        private ITF_BLL_Topic _topic;
        private ITF_BLL_File _BLL_File;
        public Admin_API_Topic(ITF_BLL_Topic topic, ITF_BLL_File BLL_File)
        {
            _BLL_File = BLL_File;
            _topic = topic;
        }

        [Route("get-Topic-by-id/{id}")]
        [HttpGet]
        public Topic GetTopicbyid(int id)
        {
            return _topic.GetTopicbyID(id);
        }

        [Route("create-Topic")]
        [HttpPost]
        public bool Create(Topic model)
        {
            return _topic.Create(model);
        }


        [Route("update-Topic")]
        [HttpPost]
        public bool Update(Topic model)
        {
            return _topic.Update(model);
        }


        [Route("Delete-Topic{id}")]
        [HttpDelete]
        public Topic Delete(int id)
        {
            return _topic.Delete(id);
        }


        [Route("Deletes-Topic")]
        [HttpDelete]
        public LIST_Topic Deletes_Topic([FromBody] LIST_Topic model)
        {
            _topic.Deletes_Topic(model);
            return model;
        }

        [Route("Search-Topic")]
        [HttpPost]
        public IActionResult Search_Topic([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _topic.Search_Topic(page, pageSize, out total, Keywords);
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

        [Route("Upload-Topic")]
        [HttpPut]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"Topic/{file.FileName.Replace("-", "_").Replace("%", "")}";
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

