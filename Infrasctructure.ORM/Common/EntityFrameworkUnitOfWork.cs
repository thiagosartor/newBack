using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Factorys;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Infrastructure.DAO.ORM.Common
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private EntityFrameworkContext dbContext = null;

        private readonly EntityFrameworkFactory dbFactory;

        protected EntityFrameworkContext DbContext
        {
            get
            {
                return dbContext ?? dbFactory.Get();
            }
        }

        public EntityFrameworkUnitOfWork(EntityFrameworkFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    DbContext.SaveChanges();

                    saveFailed = false;
                }
                catch (Exception)
                {
                    saveFailed = true;

                    var context = ((IObjectContextAdapter)DbContext).ObjectContext;

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

        public void Rollback()
        {
            DbContext.Dispose();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}