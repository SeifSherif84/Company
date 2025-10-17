using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First Name Is Required !")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName Is Required !")]
        public string? UserName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email Is Not Valid !")]
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
