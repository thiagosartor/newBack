using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Factorys;
using Infrastructure.DAO.ORM.Common.Base;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class AlunoRepositoryEF : RepositoryBaseEF<Aluno>, IAlunoRepository
    {
        public AlunoRepositoryEF(UnitOfWorkFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Aluno> GetAllByTurmaId(int id)
        {
            return GetQueryable()
                    .Include(x => x.Turma)
                    .Include(x => x.Presencas)
                    .Where(x => x.Turma.Id == id)
                    .ToList();
        }
    }
}