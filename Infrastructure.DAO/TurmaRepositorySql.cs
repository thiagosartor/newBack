using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.DAO.SQL.Repositories
{
    public class TurmaRepositorySql : ITurmaRepository
    {

        public AdoNetUnitOfWork _uow;
        public RepositoryBase<Turma> _repo;
        public TurmaRepositorySql(IUnitOfWork uow, RepositoryBase<Turma> repo)
        {
            _uow = (AdoNetUnitOfWork)uow;
            _repo = repo;
        }

        #region Querys

        public const string SqlInsert =
            "INSERT INTO TBTurma (Ano) " +
                         "VALUES ({0}Ano)";

        public const string SqlUpdate =
         "UPDATE TBTurma SET Ano = {0}Ano " +
                      "WHERE Id = {0}Id";

        public const string SqlDelete =
         "DELETE FROM TBTurma " +
                       "WHERE Id = {0}Id";

        public const string SqlSelect =
         "SELECT * FROM TBTurma";

        public const string SqlSelectbId =
        "SELECT * FROM TBTurma WHERE Id = {0}Id";

        #endregion Querys

        public Turma Add(Turma turma)
        {
            try
            {
                Db.Insert(SqlInsert, Take(turma));
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
                Db.Delete(SqlDelete, Take(turmaRemovida));
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
                Db.Delete(SqlDelete, Take(turmaRemovida));
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
                return Db.GetAll<Turma>(SqlSelect, Make);
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
                var parms = new object[] { "Id", id };

                return Db.Get(SqlSelectbId, Make, parms);
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
                Db.Update(SqlUpdate, Take(entity));
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