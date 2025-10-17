using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ForgetPasswordByEmailDto
    {

        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                           ErrorMessage = "Email Must Be In A Valid Format Like example@domain.com")]
        public string Email { get; set; }

    }
}
