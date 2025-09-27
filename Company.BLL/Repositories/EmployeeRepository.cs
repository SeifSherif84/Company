using Company.BLL.Interfaces;
using Company.DAL.Data.DBContexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _CompanyDbContext;

        public EmployeeRepository(CompanyDbContext context) : base(context) 
        {
            _CompanyDbContext = context;
        }

        public IEnumerable<Employee> GetByName(string? SearchInput)
        {
            return _CompanyDbContext.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(SearchInput.ToLower()));
        }

        #region Old Code
        //private readonly CompanyDbContext _CompanyDbContext;

        //public EmployeeRepository(CompanyDbContext CompanyDbContext)
        //{
        //    _CompanyDbContext = CompanyDbContext;
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _CompanyDbContext.Employees;
        //}

        //public Employee? GetById(int id)
        //{
        //    return _CompanyDbContext.Employees.Find(id);
        //}

        //public int Add(Employee Employee)
        //{
        //    _CompanyDbContext.Employees.Add(Employee);
        //    return _CompanyDbContext.SaveChanges();
        //}

        //public int Update(Employee Employee)
        //{
        //    _CompanyDbContext.Employees.Update(Employee);
        //    return _CompanyDbContext.SaveChanges();
        //}

        //public int Delete(Employee Employee)
        //{
        //    _CompanyDbContext.Employees.Remove(Employee);
        //    return _CompanyDbContext.SaveChanges();
        //}

        //public Employee? GetByName(string name)
        //{
        //    throw new NotImplementedException();
        //} 
        #endregion

    }
}
