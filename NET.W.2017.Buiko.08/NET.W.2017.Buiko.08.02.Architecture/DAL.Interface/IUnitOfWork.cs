using System;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
