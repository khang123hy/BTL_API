namespace Models
{
    public partial class User
    {
        public int ID_User { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }

    }

}
