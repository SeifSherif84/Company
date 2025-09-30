using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepository _employeeRepository { get; }
        IDepartmentRepository _departmentRepository { get; }
        public Task<int> SaveAsync();

    }
}
