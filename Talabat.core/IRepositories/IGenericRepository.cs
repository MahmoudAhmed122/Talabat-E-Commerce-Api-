using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Core.IGenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task <IReadOnlyList<T>> GetAllAsyncWithSpecification(ISpecification<T> spec);

        Task<T> GetByIdAsyncWithSpecification(ISpecification<T> spec);

        Task<int> CountWithSpecificationAsync(ISpecification<T> spec);  

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        Task Add(T item);
        void Update(T item);    
        void Delete(T item);    
    }
}
 