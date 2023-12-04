namespace DTO
{
    public partial class User
    {

        public int ID_User { get; set; }
        public string USER_NAME { get; set; } = null!;
        public string PASSWORD { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public int? Role { get; set; }

        public User()
        {

        }

        public User(int iD_User, string uSER_NAME, string pASSWORD, string email, string? fullName, int? role)
        {
            ID_User = iD_User;
            USER_NAME = uSER_NAME;
            PASSWORD = pASSWORD;
            Email = email;
            FullName = fullName;
            Role = role;
        }


    }
}
