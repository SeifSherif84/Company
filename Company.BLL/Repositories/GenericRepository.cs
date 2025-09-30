using Company.BLL.Interfaces;
using Company.DAL.Data.DBContexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://chatgpt.com/share/68da35d5-6d18-800f-8986-b55fbb334cba

namespace Company.BLL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompanyDbContext _CompanyDbContext;

        public GenericRepository(CompanyDbContext context)
        {
            _CompanyDbContext = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(Employee))
                return (IEnumerable<TEntity>) await _CompanyDbContext.Employees.Include(E => E.Department).ToListAsync();

            return await _CompanyDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            if (typeof(TEntity) == typeof(Employee))
                return await _CompanyDbContext.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as TEntity;

            return await _CompanyDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity model)
        {
            await _CompanyDbContext.Set<TEntity>().AddAsync(model);
        }

        public void Update(TEntity model)
        {
            _CompanyDbContext.Set<TEntity>().Update(model);
        }

        public void Delete(TEntity model)
        {
            _CompanyDbContext.Set<TEntity>().Remove(model);
        }

        //public int Save()
        //{
        //    return _CompanyDbContext.SaveChanges();
        //}

    }
}
