using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Infrastructure.DAO.ORM.Common
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DiarioAcademiaContext _dbContext;

        public EFUnitOfWork(DiarioAcademiaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    _dbContext.SaveChanges();

                    saveFailed = false;
                }
                catch (Exception)
                {
                    saveFailed = true;

                    var context = ((IObjectContextAdapter)_dbContext).ObjectContext;

                    var refreshableObjects = (from entry in context.ObjectStateManager.GetObjectStateEntries(
                                                                 EntityState.Added
                                                               | EntityState.Deleted
                                                               | EntityState.Modified
                                                               | EntityState.Unchanged)
                                              where entry.EntityKey != null && entry.Entity != null
                                              select entry.Entity)

                                              .ToList();

                    context.Refresh(RefreshMode.StoreWins, refreshableObjects);

                    context.SaveChanges();
                }
            } while (saveFailed);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}