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
        [Route("Delete-User")]
        [HttpDelete]
        public User Delete_User(int id)
        {
            return _BLL_Users.Delete_User(id);
        }

        [Route("Update-User")]
        [HttpPost]
        public bool Update_User(User model)
        {
            return _BLL_Users.Update_User(model);
        }

        [Route("Create-User")]
        [HttpPost]
        public bool Create_User(User2 model)
        {
            return _BLL_Users.Create_User(model);
        }

        [Route("Deletes-User")]
        [HttpPost]
        public List_User Deletes_User([FromBody] List_User model)
        {
            _BLL_Users.Deletes_User(model);
            return model;
        }

        ////Account
        [Route("Update-Account")]
        [HttpPost]
        public bool Update_Account(Account model)
        {
            return _BLL_Users.Update_Account(model);
        }

        [Route("Deletes-Account")]
        [HttpDelete]
        public List_User Deletes_Account([FromBody] List_User model)
        {
            _BLL_Users.Deletes_Account(model);
            return model;
        }

        [Route("Delete-Account")]
        [HttpDelete]
        public Account Delete_Account(int id)
        {
            return _BLL_Users.Delete_Account(id);
        }

    }
}
