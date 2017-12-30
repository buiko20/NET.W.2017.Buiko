using System;
using System.Data.Entity;
using DAL.Interface;

namespace DAL
{
    /// <inheritdoc />
    public class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed;

        /// <summary>
        /// Initializes the class with the passed parameters.
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Db contex.
        /// </summary>
        public DbContext DbContext { get; }

        /// <inheritdoc />
        public void Commit()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork));
            }

            this.DbContext?.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    this.DbContext?.Dispose();
                }
            }
            finally
            {
                _isDisposed = true;
            }
        }
    }
}
