using Infrasctructure.DAO.ORM.Contexts;
using System;

namespace Infrastructure.DAO.ORM.Repositories
{
    public interface IDatabaseFactory : IDisposable
    {
        DiarioAcademiaContext Get();
    }
}