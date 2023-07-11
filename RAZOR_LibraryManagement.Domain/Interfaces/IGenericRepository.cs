using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<TDestiny>> GetAllProfiled<TDestiny>();
        Task<TDestiny> GetByIdProfiled<TDestiny>(int id);
        IEnumerable<TDestiny> Get<TDestiny>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T GetByID(object id);
        TDestiny Insert<TDestiny>(TDestiny entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        TDestiny Update<TDestiny>(TDestiny entityToUpdate);
    }
}
