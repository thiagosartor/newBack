using Infrasctructure.DAO.Contexts;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Infrastructure.DAO.Common
{
    public class ADOUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _ownsConnection;


        public ADOUnitOfWork(IDbConnection connection, bool ownsConnection)
        {
            _connection = connection;
            _ownsConnection = ownsConnection;
            _transaction = connection.BeginTransaction();
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
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");

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
}