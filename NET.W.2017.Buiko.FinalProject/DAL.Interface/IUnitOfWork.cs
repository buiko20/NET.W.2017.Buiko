using System;

namespace DAL.Interface
{
    /// <summary>
    /// Unit of work pattern for 
    /// <see cref="IAccountRepository"/> and <see cref="IBankUserRepository"/>.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// commit all changes.
        /// </summary>
        void Commit();
    }
}
