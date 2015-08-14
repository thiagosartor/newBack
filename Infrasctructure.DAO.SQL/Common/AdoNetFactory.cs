using Infrastructure.DAO.Common;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Infrastructure.DAO.SQL.Common
{
    public class AdoNetFactory : UnitOfWorkFactory
    {
        #region Attributos

        private static readonly string connectionStringName =
            ConfigurationManager.AppSettings.Get("connectionDB");

        private static readonly string providerName =
            ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        private static readonly DbProviderFactory factory =
            DbProviderFactories.GetFactory(providerName);

        #endregion Attributos

        public override IUnitOfWork Create()
        {
            var connection = new SqlConnection(connectionString);

            return new ADOUnitOfWork(connection);
        }
    }
}