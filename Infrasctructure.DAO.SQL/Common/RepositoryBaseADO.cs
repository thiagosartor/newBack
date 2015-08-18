using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Infrastructure.DAO.SQL
{
    public delegate T ConverterDelegate<T>(IDataReader reader);

    public class RepositoryBaseADO
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

        private IDbConnection _connection;

        public RepositoryBaseADO(IDbConnection connection)
        {
            _connection = connection;
        }

        public static int Insert(string sql, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    SetParameters(command, parms);                   // Extension method
                    command.CommandText = AppendIdentitySelect(sql); // Extension method

                    connection.Open();

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
        }

        public static void Update(string sql, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    SetParameters(command, parms);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string sql, object[] parms = null)
        {
            Update(sql, parms);
        }

        public static List<T> GetAll<T>(string sql, ConverterDelegate<T> convert, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    SetParameters(command, parms);

                    connection.Open();

                    var list = new List<T>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var obj = convert(reader);
                        list.Add(obj);
                    }

                    return list;
                }
            }
        }

        public static T Get<T>(string sql, ConverterDelegate<T> convert, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    SetParameters(command, parms);  // Extension method

                    connection.Open();

                    T t = default(T);

                    var reader = command.ExecuteReader();

                    if (reader.Read())
                        t = convert(reader);

                    return t;
                }
            }
        }

        #region Private methods

        private static void SetParameters(DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = GetParameterPrefix() + parms[i].ToString();

                    if (parms[i + 1] is string && (string)parms[i + 1] == "")
                        parms[i + 1] = null;

                    object value = parms[i + 1] ?? DBNull.Value;

                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = name;
                    dbParameter.Value = value;

                    command.Parameters.Add(dbParameter);
                }
            }
        }

        private static string AppendIdentitySelect(string sql)
        {
            switch (providerName)
            {
                // Microsoft Access não tem suporte a esse tipo de comando
                case "System.Data.OleDb": return sql;
                case "System.Data.SqlClient": return sql + ";SELECT SCOPE_IDENTITY()";
                case "System.Data.OracleClient": return sql + ";SELECT MySequence.NEXTVAL FROM DUAL";
                default: return sql + ";SELECT @@IDENTITY";
            }
        }

        private static string GetParameterPrefix()
        {
            switch (providerName)
            {
                // Microsoft Access não tem suporte a esse tipo de comando
                case "System.Data.OleDb": return "@";
                case "System.Data.SqlClient": return "@";
                case "System.Data.OracleClient": return ":";
                case "MySql.Data.MySqlClient": return "?";

                default:
                    return "@";
            }
        }

        #endregion Private methods
    }
}