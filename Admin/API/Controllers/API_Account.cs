using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _BLL_Account.Login(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            return Ok(new { ID_User = user.ID_User, FullName = user.FullName, AccountName = user.AccountName, Password = user.Password, Email = user.Email, Phone = user.PhoneNumber, Token = user.Token });
        }


    }
}
