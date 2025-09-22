using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> GetAll();
        public T? GetById(int id);
        public int Add(T department);
        public int Update(T department);
        public int Delete(T department);
    }
}
