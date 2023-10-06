using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.IRepositories;
using Talabat.Core.Entities;
using Talabat.Core.IGenericRepository;
using Talabat.Repository.Data;
using Talabat.Repository.Repository;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public StoreContext Context { get; }

        public UnitOfWork(StoreContext context)
        {
            Context = context;
            repositories=new Hashtable();   
        }

        private Hashtable repositories; // Key:Type  , Value : GenericRepository<Type>

        public async Task<int> Complete() => await Context.SaveChangesAsync();
        

        public async ValueTask DisposeAsync() =>await Context.DisposeAsync();
        

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type)) {
                var repository = new GenericRepository<TEntity>(Context);
            
            repositories.Add(type, repository);
            }
            return repositories[type] as GenericRepository<TEntity>;
        }

    }
}
