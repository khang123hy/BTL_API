using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Account : ControllerBase
    {
        private ITF_BLL_Account _BLL_Account;
        public User_API_Account(ITF_BLL_Account bLL_Account)
        {
            _BLL_Account = bLL_Account;
        }
        [Route("CheckAccount-Account")]
        [HttpPost]
        public Check check_AccountName(string AccountName, string Email)
        {
            return _BLL_Account.check_AccountName(AccountName, Email);
        }
        ////Account
        [Route("Create-Account")]
        [HttpPost]
        public IActionResult Create_Account(Account model)
        {
            try
            {
                bool success = _BLL_Account.Create_Account(model);

                if (success)
                {
                    return Ok(new { Success = true });
                }
                else
                {
                    // Nếu có lỗi, xây dựng phản hồi lỗi
                    var errorResponse = new ErrorNotification
                    {
                        ErrorMessage = $"Đã xảy ra lỗi khi tạo tài khoản. Vui lòng thử lại."
                    };

                    return BadRequest(errorResponse);
                }
            }
            catch (Exception ex)
            {
                // Ghi nhật ký ngoại lệ hoặc xử lý nó một cách thích hợp
                var errorResponse = new ErrorNotification
                {
                    ErrorMessage = $"{ex.Message}"
                };
                return StatusCode(500, errorResponse);
            }
        }



    }
}
