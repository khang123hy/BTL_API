using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class API_Users : ControllerBase
    {
        private ITF_BLL_Users _BLL_Users;
        public API_Users(ITF_BLL_Users bll_users)
        {
            _BLL_Users = bll_users;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public User GetUser_byID(int id)
        {
            return _BLL_Users.GetUser_byID(id);
        }

        [Route("create-user")]
        [HttpPost]
        public User CreateUser([FromBody] User model)
        {
            _BLL_Users.Create_User(model);
            return model;
        }

        [Route("update-user")]
        [HttpPost]
        public User UpdateUser([FromBody] User model)
        {
            _BLL_Users.Update_User(model);
            return model;
        }
    }
}
