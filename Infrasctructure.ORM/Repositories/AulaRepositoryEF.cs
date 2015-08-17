using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.ORM.Common.Base;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class AulaRepositoryEF : RepositoryBaseEF<Aula>, IAulaRepository
    {
        public AulaRepositoryEF(IDatabaseFactory dbFactory) : base(dbFactory)
        {

        }

        public IList<Aula> GetAllByTurma(int ano)
        {
            return GetQueryable()
                .Include(t => t.Data.Year.Equals(ano))
                .ToList();
        }

        public Aula GetByData(DateTime data)
        {
            return GetQueryable()
                .FirstOrDefault(a => a.Data.Equals(data));
        }
    }
}