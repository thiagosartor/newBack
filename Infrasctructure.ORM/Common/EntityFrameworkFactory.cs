using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common.Context;

namespace NDDigital.DiarioAcademia.Infraestrutura.Orm.Common
{
    public class EntityFrameworkFactory : Disposable, IDatabaseFactory<EntityFrameworkContext>
    {
        private EntityFrameworkContext dataContext;

        public EntityFrameworkContext Get()
        {
            return dataContext ?? (dataContext = new EntityFrameworkContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}