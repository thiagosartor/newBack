using Domain.Contracts;
using Infrastructure.DAO.SQL.Common;
using Infrastructure.DAO.SQL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class AulaADOTest
    {
        public ITurmaRepository _repoTurma;
        public IAulaRepository _repoAula;
        private ADOUnitOfWork _uow;

        [TestInitialize]
        public void Initialize()
        {
            new BaseSQLTest();

            var factory = new AdoNetFactory();

            _uow = new ADOUnitOfWork(factory);

            _repoTurma = new TurmaRepositorySql(factory);

            _repoAula = new AulaRepositorySql(factory);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aula")]
        public void Deveria_Persistir_Aula_SQL_Test()
        {
            _repoTurma.Add(ObjectMother.CreateTurma());

            var turmaEncontrada = _repoTurma.GetById(1);

            var aula = ObjectMother.CreateAula(turmaEncontrada);

            aula.Turma = turmaEncontrada;

            _repoAula.Add(aula);

            var aulas = _repoAula.GetAll();

            _uow.Commit();

            Assert.AreEqual(2, aulas.Count);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aula")]
        public void Deveria_Buscar_Aula_SQL_Test()
        {
            var aulaEncontrada = _repoAula.GetById(1);

            Assert.IsNotNull(aulaEncontrada);
            Assert.AreEqual(1, aulaEncontrada.Id);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aula")]
        public void Deveria_Editar_Aula_SQL_Test()
        {
            var aulaEncontrada = _repoAula.GetById(1);
            aulaEncontrada.Data = DateTime.Now.AddYears(-15);

            _repoAula.Update(aulaEncontrada);

            var aulaEditada = _repoAula.GetById(1);

            _uow.Commit();

            Assert.AreEqual(2000, aulaEditada.Data.Year);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aula")]
        public void Deveria_Buscar_Todas_Aulas_SQL_Test()
        {
            var aulasEncontradas = _repoAula.GetAll();

            _uow.Commit();

            Assert.AreEqual(1, aulasEncontradas.Count);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aula")]
        public void Deveria_Remover_Aula_SQL_Test()
        {
            _repoAula.Delete(1);

            var aulasEncontradas = _repoAula.GetAll();

            _uow.Commit();

            Assert.IsTrue(aulasEncontradas.Count == 0);
        }
    }
}