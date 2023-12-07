namespace Models
{
    public partial class Account
    {

        public int ID_Acount { get; set; }
        public int ID_User { get; set; }
        public string AccountName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? Role { get; set; }

        public Account(int iD_Acount, int iD_User, string accountName, string password, string email, int? role)
        {
            ID_Acount = iD_Acount;
            ID_User = iD_User;
            AccountName = accountName;
            Password = password;
            Email = email;
            Role = role;
        }




    }
}
