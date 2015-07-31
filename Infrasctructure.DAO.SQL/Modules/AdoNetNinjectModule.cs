using Domain.Contracts;
using Infrastructure.DAO.SQL.Repositories;
using Ninject.Modules;

namespace Infrasctructure.DAO.ORM.Modules
{
    public class AdoNetNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITurmaRepository>().To<TurmaRepositorySql>();
            Bind<IAulaRepository>().To<AulaRepositorySql>();
            Bind<IAlunoRepository>().To<AlunoRepositorySql>();
            Bind<IPresencaRepository>().To<PresencaRepositorySql>();
        }
    }
}