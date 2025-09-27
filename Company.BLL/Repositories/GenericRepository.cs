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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompanyDbContext _CompanyDbContext;

        public GenericRepository(CompanyDbContext context)
        {
            _CompanyDbContext = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            if(typeof(TEntity) == typeof(Employee))
                return (IEnumerable<TEntity>) _CompanyDbContext.Employees.Include(E => E.Department);

            return _CompanyDbContext.Set<TEntity>();
        }

        public TEntity? GetById(int id)
        {
            if (typeof(TEntity) == typeof(Employee))
                return _CompanyDbContext.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as TEntity;

            return _CompanyDbContext.Set<TEntity>().Find(id);
        }

        public int Add(TEntity model)
        {
            _CompanyDbContext.Set<TEntity>().Add(model);
            return _CompanyDbContext.SaveChanges();
        }


        public int Update(TEntity model)
        {
            _CompanyDbContext.Set<TEntity>().Update(model);
            return _CompanyDbContext.SaveChanges();
        }

        public int Delete(TEntity model)
        {
            _CompanyDbContext.Set<TEntity>().Remove(model);
            return _CompanyDbContext.SaveChanges();
        }


    }
}
