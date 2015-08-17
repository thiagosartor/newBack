using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.DAO.Common
{
    public class ADOUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public ADOUnitOfWork(IDbConnection connection)
        {
            _connection = connection;

            _connection.Open();

            _transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Erro na transação!");
            }

            _transaction.Commit();
            _transaction = null;
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}