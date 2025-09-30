using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly CompanyDbContext _CompanyDbContext;
        public IEmployeeRepository _employeeRepository { get; }
        public IDepartmentRepository _departmentRepository { get; }

        public UnitOfWork(CompanyDbContext context
                          //IEmployeeRepository employeeRepository,
                          //IDepartmentRepository departmentRepository
                         )
        {
            _CompanyDbContext = context;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _employeeRepository = new EmployeeRepository(_CompanyDbContext);
            _departmentRepository = new DepartmentRepository(_CompanyDbContext);
        }

        public async Task<int> SaveAsync()
        {
            return await _CompanyDbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _CompanyDbContext.DisposeAsync();
        }
    }
}
