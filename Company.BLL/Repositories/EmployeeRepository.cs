using Company.BLL.Interfaces;
using Company.DAL.Data.DBContexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    internal class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext CompanyDbContext) : base(CompanyDbContext) 
        {
            
        }

        public Employee? GetByName(string name)
        {
            throw new NotImplementedException();
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
