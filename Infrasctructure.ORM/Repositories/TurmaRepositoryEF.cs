using Domain.Contracts;
using Domain.Entities;
using Infrastructure.DAO.ORM.Common.Base;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class TurmaRepositoryEF : RepositoryBaseEF<Turma>, ITurmaRepository
    {
        public TurmaRepositoryEF(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}