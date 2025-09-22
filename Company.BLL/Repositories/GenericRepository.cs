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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _CompanyDbContext;

        public GenericRepository(CompanyDbContext context)
        {
            _CompanyDbContext = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _CompanyDbContext.Set<T>();
        }

        public T? GetById(int id)
        {
            return _CompanyDbContext.Set<T>().Find(id);
        }

        public int Add(T model)
        {
            _CompanyDbContext.Set<T>().Add(model);
            return _CompanyDbContext.SaveChanges();
        }


        public int Update(T model)
        {
            _CompanyDbContext.Set<T>().Update(model);
            return _CompanyDbContext.SaveChanges();
        }

        public int Delete(T model)
        {
            _CompanyDbContext.Set<T>().Remove(model);
            return _CompanyDbContext.SaveChanges();
        }


    }
}
