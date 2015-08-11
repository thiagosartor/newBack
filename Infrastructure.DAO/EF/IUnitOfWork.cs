namespace Infrastructure.DAO.EF
{
    public interface IUnitOfWork
    {
        void Commit();

        void CommitAndRefreshChanges();
    }
}