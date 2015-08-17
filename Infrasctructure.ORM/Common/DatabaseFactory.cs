using Infrasctructure.DAO.ORM.Contexts;

namespace NDDigital.DiarioAcademia.Infraestrutura.Orm.Common
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DiarioAcademiaContext dataContext;

        public DiarioAcademiaContext Get()
        {
            return dataContext ?? (dataContext = new DiarioAcademiaContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}