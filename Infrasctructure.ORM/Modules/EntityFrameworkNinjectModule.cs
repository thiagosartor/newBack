using Domain.Contracts;
using Infrastructure.DAO.ORM.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctructure.DAO.ORM.Modules
{
    public class EntityFrameworkNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITurmaRepository>().To<TurmaRepositoryEF>();
            Bind<IAulaRepository>().To<AulaRepositoryEF>();
            Bind<IAlunoRepository>().To<AlunoRepositoryEF>();
            Bind<IPresencaRepository>().To<PresencaRepositoryEF>();
        }
    }
}
