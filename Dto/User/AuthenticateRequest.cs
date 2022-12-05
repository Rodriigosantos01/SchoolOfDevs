using System.ComponentModel.DataAnnotations;

namespace SchollOfDevs.Dto.User
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
