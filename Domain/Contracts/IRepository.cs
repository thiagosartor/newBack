using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Update(T entity);

        void Delete(int id);

        T GetById(int id);

        IList<T> GetAll();
    }
}