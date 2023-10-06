using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IGenericRepository;

namespace Talabat.core.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


        Task<int> Complete();
    }
}
