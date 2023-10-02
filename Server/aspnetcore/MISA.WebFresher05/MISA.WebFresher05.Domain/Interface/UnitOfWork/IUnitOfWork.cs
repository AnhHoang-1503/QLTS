using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Domain
{
    /// <summary>
    /// Interface của UnitOfWork
    /// </summary>
    /// Created by: vtahoang (19/07/2023) 
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        void BeginTransaction();

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        Task BeginTransactionAsync();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        void Commit();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// Created by: vtahoang (19/07/2023) 
        Task CommitAsync();

        /// <summary>
        /// Roll back transaction
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        void RollBack();

        /// <summary>
        /// Roll back transaction
        /// </summary>
        /// Created by: vtahoang (19/07/2023)
        Task RollBackAsync();
    }
}
