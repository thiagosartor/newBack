namespace Infrastructure.DAO.Common
{
    public abstract class UnitOfWorkFactory
    {
        public abstract IUnitOfWork Create();
    }
}