namespace Models
{
    public partial class User
    {


        public int ID_User { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }

        public User(int iD_User, string fullName, string address, DateTime dateOfBirth, string? phoneNumber)
        {
            ID_User = iD_User;
            FullName = fullName;
            Address = address;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
        }

    }
}
