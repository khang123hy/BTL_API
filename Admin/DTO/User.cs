using System.ComponentModel.DataAnnotations;

namespace DTO
{


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
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

    }

    public class User
    {
        public int ID_User { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }


    public class User2
    {
        public int ID_User { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string Avatar { get; set; }

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
