using ObjectDetection.Domain;
using ObjectDetection.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDetection.Infrastructure
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ObjectDetectionDbContext _detectionDbContext;
       
        public UnitOfWork(ObjectDetectionDbContext businessDbContext)
        {
            _detectionDbContext = businessDbContext;
        }

        public void Commit()
            => _detectionDbContext.SaveChanges();

        public async Task CommitAsync()
        {
            await _detectionDbContext.SaveChangesAsync();
        }

        public void Rollback() 
            => _detectionDbContext.Dispose();

        public async Task RollbackAsync()
            => await _detectionDbContext.DisposeAsync();

    }
}
