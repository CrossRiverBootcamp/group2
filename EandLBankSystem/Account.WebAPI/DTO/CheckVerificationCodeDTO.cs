using System.ComponentModel.DataAnnotations;

namespace Account.WebAPI.DTO
{
    public class CheckVerificationCodeDTO
    {
        [Required ,EmailAddress]
        public string Email { get; set; }
        [Required ,StringLength(4)]
        public string Code { get; set; }
    }
}
