using Infrasctructure.DAO.Contexts;
using System;

namespace Infrastructure.DAO.Common
{
    public interface IDatabaseFactory : IDisposable
    {
        DiarioAcademiaContext Get();
    }
}