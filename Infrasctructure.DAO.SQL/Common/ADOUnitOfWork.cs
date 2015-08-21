using Infrastructure.DAO.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.DAO.SQL.Common
{
    public class ADOUnitOfWork : IUnitOfWork
    {
        public AdoNetFactory _factory;
        public ADOUnitOfWork(AdoNetFactory factory)
        {
            _factory = factory;
        }

        public void Commit()
        {
            if (_factory.Command.Transaction == null)
            {
                throw new InvalidOperationException("Erro na transação!");
            }

            _factory.Command.Transaction.Commit();
            _factory.Command.Transaction = null;
        }

        public void Dispose()
        {
            Rollback();
        }

        public void Rollback()
        {
            if (_factory.Command.Transaction != null)
            {
                _factory.Command.Transaction.Rollback();
                _factory.Command.Transaction = null;
            }

            if (_factory.Connection != null)
            {
                _factory.Connection.Close();
                _factory.Connection = null;
            }
        }
    }
}