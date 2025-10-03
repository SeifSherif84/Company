using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class RegistrationDto
    {
        [Required(ErrorMessage = "Username Is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username Must Be Between 3 And 50 Characters")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "First Name Is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name Must Be Between 3 And 50 Characters")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name Is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name Must Be Between 3 And 50 Characters")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email Is Required")]
        //[EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
                           ErrorMessage = "Email Must Be In A Valid Format Like example@domain.com")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                           ErrorMessage = "Password Must Be At Least 8 Characters, Contain Uppercase, Lowercase, Number, And Special Character.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password Does not Match The Password")]
        public string ConfirmPassword { get; set; }


        public bool IsAgree { get; set; }

    }
}
