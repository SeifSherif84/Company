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
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository                                               /*IEmployeeRepository*/
    {
        public DepartmentRepository(CompanyDbContext context) : base(context)
        {

        }

        #region Old Code
        //private readonly CompanyDbContext _CompanyDbContext;
        //public DepartmentRepository(CompanyDbContext CompanyDbContext)
        //{
        //    _CompanyDbContext = CompanyDbContext;
        //}
        //public IEnumerable<Department> GetAll()
        //{
        //   return _CompanyDbContext.Departments;
        //}

        //public Department? GetById(int id)
        //{
        //    return _CompanyDbContext.Departments.Find(id);
        //}

        //public int Add(Department department)
        //{
        //    _CompanyDbContext.Departments.Add(department);
        //    return _CompanyDbContext.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    _CompanyDbContext.Departments.Update(department);
        //    return _CompanyDbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _CompanyDbContext.Departments.Remove(department);
        //    return _CompanyDbContext.SaveChanges();
        //} 
        #endregion

    }
}
