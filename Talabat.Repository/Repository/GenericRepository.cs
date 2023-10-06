using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext Context;

        public GenericRepository(StoreContext context)
        {
            Context = context;
        }

        public async Task Add(T item)=>await Context.Set<T>().AddAsync(item);      
      
        public async Task<int> CountWithSpecificationAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Delete(T item)=>Context.Set<T>().Remove(item);    
     
        public async Task<IReadOnlyList<T>> GetAllAsync() => await Context.Set<T>().ToListAsync();
        

        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpecification(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id) => await Context.Set<T>().FindAsync(id);
       
        public async Task<T> GetByIdAsyncWithSpecification(ISpecification<T> spec)
        {

            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public void Update(T item)=>Context.Set<T>().Update(item);
      

        private IQueryable<T> ApplySpecification(ISpecification<T> spec) {

            return SpecificationEvaluator<T>.GetQuery(Context.Set<T>(), spec);
        } 


    }
}
