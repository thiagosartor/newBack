using Infrastructure.DAO.SQL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Infrasctructure.DAO.SQL.Common
{
    public delegate T ConverterDelegate<T>(IDataReader reader);

    public class RepositoryBaseADO
    {
        private AdoNetFactory _factory;

        public RepositoryBaseADO(AdoNetFactory factory)
        {
            _factory = factory;
        }

        public int Insert(string sql, object[] parms = null)
        {
            sql = string.Format(sql, "@");
            sql += ";SELECT SCOPE_IDENTITY()";

            _factory.Command.SetParameters(parms);

            _factory.Command.CommandText = sql;

            int id = Convert.ToInt32(_factory.Command.ExecuteScalar());

            return id;
        }

        public void Update(string sql, object[] parms = null)
        {
            sql = string.Format(sql, "@");

            _factory.Command.CommandText = sql;
            _factory.Command.SetParameters(parms);

            _factory.Command.ExecuteNonQuery();
        }

        public void Delete(string sql, object[] parms = null)
        {
            Update(sql, parms);
        }

        public List<T> GetAll<T>(string sql, ConverterDelegate<T> convert, object[] parms = null)
        {
            sql = string.Format(sql, "@");

            _factory.Command.CommandText = sql;
            _factory.Command.SetParameters(parms);

            var list = new List<T>();
            var reader = _factory.Command.ExecuteReader();

            while (reader.Read())
            {
                var obj = convert(reader);
                list.Add(obj);
            }

            return list;
        }

        public T Get<T>(string sql, ConverterDelegate<T> convert, object[] parms = null)
        {
            sql = string.Format(sql, "@");

            _factory.Command.CommandText = sql;
            _factory.Command.SetParameters(parms);

            T t = default(T);

            var reader = _factory.Command.ExecuteReader();

            if (reader.Read())
                t = convert(reader);

            return t;
        }
    }

    public static class Helpers
    {
        public static void SetParameters(this DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = "@" + parms[i].ToString();//TODO: GetParameterPrefix()

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
    }
}