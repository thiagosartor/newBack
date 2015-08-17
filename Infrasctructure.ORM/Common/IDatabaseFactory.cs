using Infrasctructure.DAO.ORM.Contexts;
using System;

namespace NDDigital.DiarioAcademia.Infraestrutura.Orm.Common
{
    public interface IDatabaseFactory : IDisposable
    {
        DiarioAcademiaContext Get();
    }
}