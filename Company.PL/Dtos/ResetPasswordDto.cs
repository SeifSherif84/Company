using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ResetPasswordDto
    {

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                   ErrorMessage = "Password Must Be At Least 8 Characters, Contain Uppercase, Lowercase, Number, And Special Character.")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Does not Match The Password")]
        public string ConfirmPassword { get; set; }

    }
}
