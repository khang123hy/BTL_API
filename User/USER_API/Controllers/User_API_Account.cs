﻿using BLL.Interfaces;
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
        [Route("Create-Account")]
        [HttpPost]
        public bool Create_Account(Account model)
        {
            return _BLL_Account.Create_Account(model);
        }


    }
}