using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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


        ////Account
        [Route("Create-Account")]
        [HttpPost]
        public bool Create_Account(Account model)
        {
            return _BLL_Account.Create_Account(model);
        }


    }
}
