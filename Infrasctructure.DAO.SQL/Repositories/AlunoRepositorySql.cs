using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.SQL.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.DAO.SQL.Repositories
{
    public class AlunoRepositorySql : IAlunoRepository
    {
        #region Querys

        public const string SqlInsert =
         @"INSERT INTO TBAluno
               ([Endereco_Cep]
               ,[Endereco_Bairro]
               ,[Endereco_Localidade]
               ,[Endereco_Uf]
               ,[Nome]
               ,[Turma_Id])
         VALUES
               ({0}Endereco_Cep,
                {0}Endereco_Bairro,
                {0}Endereco_Localidade,
                {0}Endereco_Uf,
                {0}Nome,
                {0}Turma_Id)";

        public const string SqlUpdate =
        @"UPDATE TBAluno SET
                [Endereco_Cep] = {0}Endereco_Cep
               ,[Endereco_Bairro] = {0}Endereco_Bairro
               ,[Endereco_Localidade] = {0}Endereco_Localidade
               ,[Endereco_Uf] = {0}Endereco_Uf
               ,[Nome] = {0}Nome
               ,[Turma_Id] = {0}Turma_Id
          WHERE [Id] = {0}Id";

        public const string SqlDelete =
         "DELETE FROM TBAluno " +
                       "WHERE Id = {0}Id";

        public const string SqlSelect =
         "SELECT * FROM TBAluno";

        public const string SqlSelectAllByTurma =
       "SELECT * FROM TBAluno WHERE Turma_Id = {0}Turma_Id";

        public const string SqlSelectbId =
        "SELECT * FROM TBAluno WHERE Id = {0}Id";

        #endregion Querys

        public Aluno Add(Aluno entity)
        {
            Db.Insert(SqlInsert, Take(entity));

            return entity;
        }

        public void Delete(int id)
        {
            var alunoRemovido = GetById(id);
            Db.Delete(SqlDelete, Take(alunoRemovido));
        }

        public void Delete(Aluno entity)
        {
            Db.Delete(SqlDelete, Take(entity));
        }

        public IList<Aluno> GetAll()
        {
            return Db.GetAll(SqlSelect, Make);
        }

        public IList<Aluno> GetAllByTurma(int ano)
        {
            var parms = new object[] { "ano", ano };

            return Db.GetAll(SqlSelectAllByTurma, Make, parms);
        }

        public IList<Aluno> GetAllByTurmaId(int turmaId)
        {
            var parms = new object[] { "Turma_Id", turmaId };

            return Db.GetAll(SqlSelectAllByTurma, Make, parms);
        }

        public Aluno GetById(int id)
        {
            var parms = new object[] { "Id", id };

            return Db.Get(SqlSelectbId, Make, parms);
        }

        public void Update(Aluno entity)
        {
            Db.Update(SqlUpdate, Take(entity));
        }

        private static Aluno Make(IDataReader reader)
        {
            Aluno aluno = new Aluno();
            aluno.Id = Convert.ToInt32(reader["Id"]);
            aluno.Nome = Convert.ToString(reader["Nome"]);
            aluno.Turma.Id = Convert.ToInt32(reader["Turma_Id"]); //TODO: Tirei o protected da Entity
            aluno.Endereco.Cep = Convert.ToString(reader["Endereco_Cep"]);
            aluno.Endereco.Localidade = Convert.ToString(reader["Endereco_Localidade"]);
            aluno.Endereco.Bairro = Convert.ToString(reader["Endereco_Bairro"]);
            aluno.Endereco.Uf = Convert.ToString(reader["Endereco_Uf"]);

            return aluno;
        }

        private static object[] Take(Aluno aluno)
        {
            return new object[]
            {
                "Id", aluno.Id,
                "Nome", aluno.Nome,
                "Turma_Id", aluno.Turma.Id,
                "Endereco_Cep", aluno.Endereco.Cep,
                "Endereco_Localidade", aluno.Endereco.Localidade,
                "Endereco_Bairro", aluno.Endereco.Bairro,
                "Endereco_Uf", aluno.Endereco.Uf
            };
        }
    }
}