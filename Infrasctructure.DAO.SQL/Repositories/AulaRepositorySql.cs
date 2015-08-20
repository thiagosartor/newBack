using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.SQL.Common;
using Infrastructure.DAO.SQL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.DAO.SQL.Repositories
{
    public class AulaRepositorySql : RepositoryBaseADO, IAulaRepository
    {
        #region Querys

        public const string SqlInsert = @"INSERT INTO TBAula 
            (Data, ChamadaRealizada, Turma_Id) VALUES 
            ({0}Data, {0}ChamadaRealizada, {0}Turma_Id)";

        public const string SqlUpdate = @"UPDATE TBAula SET 
            Data = {0}Data, 
            ChamadaRealizada = {0}ChamadaRealizada, 
            Turma_Id = {0}Turma_Id          
            WHERE Id = {0}Id";

        public const string SqlDelete = @"DELETE FROM TBAula 
            WHERE Id = {0}Id";

        public const string SqlSelect = @"SELECT * FROM TBAula";

        public const string SqlSelectbId = @"SELECT * FROM TBAula 
            WHERE Id = {0}Id";

        public const string SqlSelectTexto = @"SELECT * FROM TBAula 
            WHERE Data = {0}Data";

        #endregion Querys

        public AulaRepositorySql(AdoNetFactory factory) : base(factory)
        {
                
        }

        public Aula Add(Aula entity)
        {
            try
            {
               Insert(SqlInsert, Take(entity));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar adicionar uma Aula!" + te.Message);
            }
            return entity;
        }

        public void Delete(int id)
        {
            try
            {
                var aulaRemovida = GetById(id);
               Delete(SqlDelete, Take(aulaRemovida));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar deletar uma Aula!" + te.Message);
            }
        }

        public void Delete(Aula entity)
        {
            try
            {
                var aulaRemovida = GetById(entity.Id);
               Delete(SqlDelete, Take(aulaRemovida));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar deletar uma Aula!" + te.Message);
            }
        }

        public IList<Aula> GetAll()
        {
            try
            {
                return GetAll<Aula>(SqlSelect, Make);
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar listar todas as Aulas!" + te.Message);
            }
        }

        public IList<Aula> GetAllByTurma(int ano)
        {
            throw new NotImplementedException();
        }

        public IList<Aula> GetAllIncluding(params Expression<Func<Aula, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Aula GetByData(DateTime data)
        {
            throw new NotImplementedException();
        }

        public Aula GetById(int id)
        {
            try
            {
                var parms = new object[] { "Id", id };

                return Get(SqlSelectbId, Make, parms);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Aula GetByIdIncluding(int id, params Expression<Func<Aula, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IList<Aula> GetMany(Expression<Func<Aula, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Update(Aula entity)
        {
            try
            {
               Update(SqlUpdate, Take(entity));
            }
            catch (Exception te)
            {
                throw new Exception("Erro ao tentar editar uma Aula!" + te.Message);
            }
        }

        private static Aula Make(IDataReader reader)
        {
            Aula aula = new Aula();

            aula.Id = Convert.ToInt32(reader["Id"]);
            aula.Data = Convert.ToDateTime(reader["Data"]);
            aula.ChamadaRealizada = Convert.ToBoolean(reader["ChamadaRealizada"]);
            aula.Turma.Id = Convert.ToInt32(reader["Turma_Id"]);

            return aula;
        }

        private static object[] Take(Aula aula)
        {
            return new object[]
            {
                "Id", aula.Id,
                "Data", aula.Data,
                "ChamadaRealizada", aula.ChamadaRealizada,
                "Turma_Id", aula.Turma.Id
            };
        }
    }
}