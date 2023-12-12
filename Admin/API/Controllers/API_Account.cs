using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Account : ControllerBase
    {
        private ITF_BLL_Account _BLL_Account;
        public API_Account(ITF_BLL_Account bLL_Account)
        {
            _BLL_Account = bLL_Account;
        }

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] AuthenticateModel model)
        //{
        //    var user = _BLL_Account.Login(model.Username, model.Password);
        //    if (user == null)
        //        return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
        //    return Ok(new { ID_User = user.ID_User, FullName = user.FullName, AccountName = user.AccountName, Password = user.Password, Email = user.Email, Phone = user.PhoneNumber, Token = user.Token });
        //}

        ////Account
        [Route("Update-Account")]
        [HttpPost]
        public bool Update_Account(Account model)
        {
            return _BLL_Account.Update_Account(model);
        }

        [Route("Deletes-Account")]
        [HttpDelete]
        public List_User Deletes_Account([FromBody] List_User model)
        {
            _BLL_Account.Deletes_Account(model);
            return model;
        }

        [Route("Delete-Account")]
        [HttpDelete]
        public Account Delete_Account(int id)
        {
            return _BLL_Account.Delete_Account(id);
        }

        [Route("Search-Account")]
        [HttpPost]
        public IActionResult Search_Account([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Keywords = "";
                if (formData.Keys.Contains("Keywords") && !string.IsNullOrEmpty(Convert.ToString(formData["Keywords"]))) { Keywords = Convert.ToString(formData["Keywords"]); }
                long total = 0;
                var data = _BLL_Account.Search_Account(page, pageSize, out total, Keywords);
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
