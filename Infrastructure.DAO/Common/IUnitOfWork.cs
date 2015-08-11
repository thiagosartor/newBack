namespace Infrastructure.DAO.Common
{
    public interface IUnitOfWork
    {
        void Commit();

        void CommitAndRefreshChanges();
    }
}