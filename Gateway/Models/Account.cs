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

    }



    public class AppSettings
    {
        public string Secret { get; set; }
    }


}
