using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Factorys;
using Infrastructure.DAO.ORM.Common.Base;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class TurmaRepositoryEF : RepositoryBaseEF<Turma>, ITurmaRepository
    {
        public TurmaRepositoryEF(EntityFrameworkFactory factory) : base(factory)
        {

        }
    }
}