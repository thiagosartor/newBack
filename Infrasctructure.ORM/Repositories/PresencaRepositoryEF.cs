using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Factorys;
using Infrastructure.DAO.ORM.Common.Base;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
using System;
using System.Collections.Generic;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class PresencaRepositoryEF : RepositoryBaseEF<Presenca>, IPresencaRepository
    {
        public PresencaRepositoryEF(UnitOfWorkFactory dbFactory) : base (dbFactory)
        {

        }
    }
}