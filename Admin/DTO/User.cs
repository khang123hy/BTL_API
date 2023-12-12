using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public partial class User
    {


        public int ID_User { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
    }

    public class List_User
    {
        public List<CT_User> list_json_ID_User { get; set; }

    }
    public class CT_User
    {
        public int ID_User { get; set; }
    }



    public partial class Account
    {

        public int ID_Account { get; set; }
        public int ID_User { get; set; }
        public string AccountName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; }
        public string Token { get; set; }

    }

    public class User2
    {
        public int ID_User { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public int ID_Account { get; set; }
        public string AccountName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; }
    }


    public class LoginResult
    {
        public int ID_User { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public int ID_Account { get; set; }
        public string AccountName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; }
        public string Token { get; set; }
    }

    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
