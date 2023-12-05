using System.ComponentModel.DataAnnotations;

namespace DTO
{
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
