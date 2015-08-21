using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.SQL.Common;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.SQL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.DAO.SQL.Repositories
{
    public class TurmaRepositorySql : RepositoryBaseADO, ITurmaRepository
    {

        #region Querys

        public const string SqlInsert =
            "INSERT INTO TBTurma (Ano) " +
                         "VALUES ({0}Ano)";

        public const string SqlUpdate =
         "UPDATE TBTurma SET Ano = {0}Ano " +
                      "WHERE Id = {0}Id_Turma";

        public const string SqlDelete =
         "DELETE FROM TBTurma " +
                       "WHERE Id = {0}Id_Turma";

        public const string SqlSelect =
         "SELECT * FROM TBTurma";

        public const string SqlSelectbId =
        "SELECT * FROM TBTurma WHERE Id = {0}Id_Turma";

        #endregion Querys

        public TurmaRepositorySql(UnitOfWorkFactory factory) : base(factory)
        {

        }

        public Turma Add(Turma turma)
        {
            try
            {
                Insert(SqlInsert, Take(turma));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar adicionar uma Turma!" + te.Message);
            }

            return turma;
        }

        public void Delete(int id)
        {
            try
            {
                var turmaRemovida = GetById(id);
                Delete(SqlDelete, Take(turmaRemovida));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar deletar uma Turma!" + te.Message);
            }
        }

        public void Delete(Turma entity)
        {
            try
            {
                var turmaRemovida = GetById(entity.Id);
                Delete(SqlDelete, Take(turmaRemovida));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar deletar uma Turma!" + te.Message);
            }
        }

        public IList<Turma> GetAll()
        {
            try
            {
                return GetAll<Turma>(SqlSelect, Make);
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar listar todas as Turmas!" + te.Message);
            }
        }

        public Turma GetById(int id)
        {
            try
            {
                var parms = new object[] { "Id_Turma", id };

                return Get(SqlSelectbId, Make, parms);
            }
            catch (Exception te)
            {
                return null;
                throw new Exception("Erro ao tentar encontrar a turma!" + te.Message);
            }
        }

        public void Update(Turma entity)
        {
            try
            {
                Update(SqlUpdate, Take(entity));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar editar uma Turma!" + te.Message);
            }
        }

        private static Turma Make(IDataReader reader)
        {
            Turma turma = new Turma();
            turma.Id = Convert.ToInt32(reader["Id"]);
            turma.Ano = Convert.ToInt32(reader["Ano"]);

            return turma;
        }

        private static object[] Take(Turma turma)
        {
            return new object[]
            {
                "Id", turma.Id,
                "Ano", turma.Ano
            };
        }

        public IList<Turma> GetAllIncluding(params Expression<Func<Turma, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Turma GetByIdIncluding(int id, params Expression<Func<Turma, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IList<Turma> GetMany(Expression<Func<Turma, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}