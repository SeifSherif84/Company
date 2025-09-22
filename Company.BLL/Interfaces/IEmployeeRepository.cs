using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll();
        public Employee? GetById(int id);
        public int Add(Employee Employee);
        public int Update(Employee Employee);
        public int Delete(Employee Employee);

    }
}
