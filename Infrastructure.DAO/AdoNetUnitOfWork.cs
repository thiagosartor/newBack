using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.ORM.Repositories;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Infrastructure.DAO
{
    public interface IUnitOfWork
    {
        void Commit();

        void Roolback(); 
    }

    public class AdoNetUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IDatabaseFactory _dbFactory;

        private bool _ownsConnection;

        public AdoNetUnitOfWork(IDatabaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            //_connection = connection;
            //_ownsConnection = ownsConnection;
            //_transaction = connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException
                    ("Transaction have already been commited. Check your transaction handling.");

            _transaction.Commit();
            _transaction = null;
        }

        public void Roolback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null && _ownsConnection)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }

    public class EFUnitOfWork : IUnitOfWork
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

        public EFUnitOfWork(IDatabaseFactory dbFactory)
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

        public void Roolback()
        {
            DbContext.Dispose();
        }
    }
}