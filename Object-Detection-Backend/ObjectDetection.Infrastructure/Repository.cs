using Microsoft.EntityFrameworkCore;
using ObjectDetection.Domain;
using ObjectDetection.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly ObjectDetectionDbContext _detectionDbContext;
        private readonly DbSet<T> _entitySet;


        public Repository(ObjectDetectionDbContext businessDbContext)
        {
            _detectionDbContext = businessDbContext;
            _entitySet = _detectionDbContext.Set<T>();
        }


        public void Add(T entity)
            =>_detectionDbContext.Add(entity);


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _detectionDbContext.AddAsync(entity, cancellationToken);


        public void AddRange(IEnumerable<T> entities)
            =>_detectionDbContext.AddRange(entities);


        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _detectionDbContext.AddRangeAsync(entities, cancellationToken);


        public T Get(Expression<Func<T, bool>> expression)
            => _entitySet.FirstOrDefault(expression);


        public IEnumerable<T> GetAll()
            => _entitySet.AsEnumerable();


        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
            => _entitySet.Where(expression).AsEnumerable();


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entitySet.ToListAsync(cancellationToken);


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitySet.Where(expression).ToListAsync(cancellationToken);


        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitySet.FirstOrDefaultAsync(expression, cancellationToken);


        public void Remove(T entity)
            =>_detectionDbContext.Remove(entity);


        public void RemoveRange(IEnumerable<T> entities)
            =>_detectionDbContext.RemoveRange(entities);


        public void Update(T entity)
            =>_detectionDbContext.Update(entity);


        public void UpdateRange(IEnumerable<T> entities)
            =>_detectionDbContext.UpdateRange(entities);

        public void UpdateRangeList(List<T> entities)
        {
           _detectionDbContext.UpdateRange(entities);
        }

        Task<T> IRepository<T>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
