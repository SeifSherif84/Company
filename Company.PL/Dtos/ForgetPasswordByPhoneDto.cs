using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ForgetPasswordByPhoneDto
    {
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }
    }
}
