using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.ORM.Common.Base;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class AlunoRepositoryEF : RepositoryBaseEF<Aluno>, IAlunoRepository
    {
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