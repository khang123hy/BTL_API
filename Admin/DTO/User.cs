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


    }
    public partial class Account
    {

        public int ID_Account { get; set; }
        public int ID_User { get; set; }
        public string AccountName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; }
        public string Token { get; set; }


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

}
