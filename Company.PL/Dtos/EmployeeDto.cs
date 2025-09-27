using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Company.DAL.Models;

namespace Company.PL.Dtos
{
    public class EmployeeDto
    {
        [Required(ErrorMessage = "Name Is Required !")]
        public string Name { get; set; }

        [Range(20, 60, ErrorMessage = "Age Is Not Valid !")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email Is Not Valid !")]
        public string Email { get; set; }

        [RegularExpression(pattern: @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                          , ErrorMessage = "Address Is Must Like 123-Street,City-Country !")]
        public string Address { get; set; }

        //[DataType(DataType.PhoneNumber)]
        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime CreateAt { get; set; }



        [DisplayName("Department")]
        public int? DeptId { get; set; }

        [DisplayName("Department")]
        public string? DepartmentName { get; set; }
    }
}
