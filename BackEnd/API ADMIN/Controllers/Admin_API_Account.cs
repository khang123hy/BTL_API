﻿using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_Account : ControllerBase
    {
        private ITF_BLL_Account _BLL_Account;
        public Admin_API_Account(ITF_BLL_Account bLL_Account)
        {
            _BLL_Account = bLL_Account;
        }


        [Route("CheckAccount-Account")]
        [HttpPost]
        public Check check_AccountName(string AccountName, string Email)
        {
            return _BLL_Account.check_AccountName(AccountName, Email);
        }
        //Account
        [Route("Create-Account")]
        [HttpPost]
        public bool Create_Account(Account model)
        {
            return _BLL_Account.Create_Account(model);
        }


    }
}
