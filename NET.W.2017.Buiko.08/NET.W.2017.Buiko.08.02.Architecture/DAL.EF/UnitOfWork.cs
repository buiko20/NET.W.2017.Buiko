using System;
using System.Data.Entity;
using DAL.Interface;

namespace DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed;

        public DbContext DbContext { get; }

        public UnitOfWork(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void Commit()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork));
            }

            this.DbContext?.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        //~UnitOfWork()
        //{
        //    this.Dispose(false);
        //}

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
