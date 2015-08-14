using Infrastructure.DAO.Common;

namespace Infrastructure.DAO.ORM.Common
{
    public class EntityFrameworkFactory : UnitOfWorkFactory
    {
        public override IUnitOfWork Create()
        {
            return new EFUnitOfWork();
        }
    }
}