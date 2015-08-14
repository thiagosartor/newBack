using System;

namespace Infrastructure.DAO.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}