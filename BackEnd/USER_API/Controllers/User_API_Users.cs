using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace USER_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class User_API_Users : ControllerBase
    {
        private ITF_BLL_Users _BLL_Users;
        public User_API_Users(ITF_BLL_Users bll_users)
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
        public bool Update_User(User2 model)
        {
            return _BLL_Users.Update_User(model);
        }



    }
}
