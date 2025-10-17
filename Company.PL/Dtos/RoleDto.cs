using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class RoleDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name Is Required !")]
        public string? Name { get; set; }

    }
}
