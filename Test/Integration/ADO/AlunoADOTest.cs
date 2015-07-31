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
            _repoAluno = new AlunoRepositorySql();
            _repoTurma = new TurmaRepositorySql();
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Persistir_Aluno_Test()
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
        public void Deveria_Buscar_Aluno_Test()
        {
            var alunoEncontrado =  _repoAluno.GetById(1);

            Assert.IsNotNull(alunoEncontrado);
            Assert.AreEqual(1, alunoEncontrado.Id);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Editar_Aluno_Test()
        {
            var alunoEncontrado = _repoAluno.GetById(1);
            alunoEncontrado.Nome = "Alexandre Regis";

            _repoAluno.Update(alunoEncontrado);

            var alunoEditada = _repoAluno.GetById(1);

            Assert.AreEqual("Alexandre Regis", alunoEditada.Nome);

        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Buscar_Todas_Alunos_Test()
        {
            var alunosEncontrados = _repoAluno.GetAll();

            Assert.IsNotNull(alunosEncontrados);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Remover_Aluno_Test()
        {
            _repoAluno.Delete(1);

            var alunosEncontrados = _repoAluno.GetAll();

            Assert.IsNull(alunosEncontrados);
        }
    }
}
