using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAO.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private DiarioAcademiaContext dbContext = null;

        private readonly IDatabaseFactory dbFactory;

        protected DiarioAcademiaContext DbContext
        {
            get
            {
                return dbContext ?? dbFactory.Get();
            }
        }

        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public void Commit()
        {
            //try
            //{
            DbContext.SaveChanges();
            //}
            //catch (OptimisticConcurrencyException ocException)
            //{
            //    var context = ((IObjectContextAdapter)DbContext).ObjectContext;

            //    var refreshableObjects = (from entry in context.ObjectStateManager.GetObjectStateEntries(
            //                                                EntityState.Added
            //                                               | EntityState.Deleted
            //                                               | EntityState.Modified
            //                                               | EntityState.Unchanged)
            //                              where entry.EntityKey != null
            //                              select entry.Entity).ToList();

            //    context.Refresh(RefreshMode.StoreWins, refreshableObjects);

            //    context.SaveChanges();
            //}
            //catch (Exception exc) {
            //}
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

        public void Roolback()
        {
            DbContext.Dispose();
        }
    }
}
