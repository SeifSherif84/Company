using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity? GetById(int id);
        public int Add(TEntity department);
        public int Update(TEntity department);
        public int Delete(TEntity department);

    }
}
