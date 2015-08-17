using Infrastructure.DAO.Common;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;

namespace Infrastructure.DAO.ORM.Common
{
    public class EntityFrameworkFactory : UnitOfWorkFactory
    {
        public IDatabaseFactory _factory;

        public override IUnitOfWork Create()
        {
            return new EFUnitOfWork(_factory);
        }
    }
}