using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.DAO.ORM.Repositories;
using System.Data.Entity;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.SQL.Repositories;

namespace Test
{
    [TestClass]
    public class AlunoADOTest
    {
        public AlunoRepositorySql _repoAluno;
        public TurmaRepositorySql _repoTurma;

        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(new BaseEFTest());

            new BaseSQLTest();

            _repoAluno = new AlunoRepositorySql();
            _repoTurma = new TurmaRepositorySql();
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Persistir_Aluno_SQL_Test()
        {
            _repoTurma.Add(ObjectMother.CreateTurma());

            var turmaEncontrada = _repoTurma.GetById(1);

            var aluno = ObjectMother.CreateAluno(turmaEncontrada);

            aluno.Turma = turmaEncontrada;

            _repoAluno.Add(aluno);

            Assert.IsNotNull(aluno);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Buscar_Aluno_SQL_Test()
        {
            var alunoEncontrado =  _repoAluno.GetById(1);

            Assert.IsNotNull(alunoEncontrado);
            Assert.AreEqual(1, alunoEncontrado.Id);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Editar_Aluno_SQL_Test()
        {
            var alunoEncontrado = _repoAluno.GetById(1);
            alunoEncontrado.Nome = "Alexandre Regis";

            _repoAluno.Update(alunoEncontrado);

            var alunoEditado = _repoAluno.GetById(1);

            Assert.AreEqual("Alexandre Regis", alunoEditado.Nome);

        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Buscar_Todas_Alunos_SQL_Test()
        {
            var alunosEncontrados = _repoAluno.GetAll();

            Assert.IsNotNull(alunosEncontrados);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Remover_Aluno_SQL_Test()
        {
            _repoAluno.Delete(1);

            var alunosEncontrados = _repoAluno.GetAll();

            Assert.IsTrue(alunosEncontrados.Count == 0);
        }
    }
}
