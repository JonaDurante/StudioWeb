using System.ComponentModel.DataAnnotations;

namespace StudioData.Models.Loggin
{
    public class UserLoggin
    {
        [Required]
        public string UserMail { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
