namespace Infrastructure.DAO
{
    public interface IUnitOfWork
    {
        void Commit();

        void Roolback();
    }
}