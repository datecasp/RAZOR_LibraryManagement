using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Models;
using System.Data;
using System.Linq.Expressions;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly LM_DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public GenericRepository(LM_DbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDestiny>> GetAllProfiled<TDestiny>()
        {
            var elements = _dbSet.ToList();
            var result = MapTo<List<TDestiny>> (elements);
            return result;
        }

        public async Task<TDestiny> GetByIdProfiled<TDestiny>(int id)
        {
            var element = _dbSet.Find(id);
            var result = MapTo<TDestiny>(element);
            return result;
        }

        public virtual IEnumerable<TDestiny> Get<TDestiny>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return MapTo<List<TDestiny>>(orderBy(query));
            }
            else
            {
                return MapTo<List<TDestiny>>(query);
            }
        }

        public virtual T GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual TDestiny Insert<TDestiny>(TDestiny entity)
        {
            var res = _mapper.Map<T>(entity);
            res = _dbSet.Add(res).Entity;
            var resModel = _mapper.Map<TDestiny>(res);
            return resModel;
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public TDestiny Update<TDestiny>(TDestiny entityToUpdate)
        {
            var res = MapTo<T>(entityToUpdate);
            _dbSet.Attach(res);
            _context.Entry(res).State = EntityState.Modified;
            var resModel = _mapper.Map<TDestiny>(res);
            return resModel;
        }

        // Mapping T <-> TagretObject
        public TDestination MapTo<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}