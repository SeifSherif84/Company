using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreatedAt Is Required")]
        public DateTime CreatedAt { get; set; }
    }
}
