﻿using Infrastructure.DAO.SQL.Common;
using Infrastructure.DAO.SQL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TurmaADOTest
    {
        private TurmaRepositorySql _repo;
        private ADOUnitOfWork _uow;
        private AdoNetFactory _factory;

        [TestInitialize]
        public void Initialize()
        {
            new BaseSQLTest();

            _factory = new AdoNetFactory();

            _uow = new ADOUnitOfWork(_factory);         

            _repo = new TurmaRepositorySql(_factory);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Persistir_Turma_SQL_Test()
        {
            var turma = ObjectMother.CreateTurma();

            _repo.Add(turma);

            var qtdTurmas = _repo.GetAll().Count;

            _uow.Commit();

            Assert.AreEqual(2, qtdTurmas);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Buscar_Turma_SQL_Test()
        {
            var turmaEncontrada = _repo.GetById(1);

            Assert.IsNotNull(turmaEncontrada);
            Assert.AreEqual(1, turmaEncontrada.Id);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Editar_Turma_SQL_Test()
        {
            var turmaEncontrada = _repo.GetById(1);
            turmaEncontrada.Ano = 2016;

            _repo.Update(turmaEncontrada);

            var turmaEditada = _repo.GetById(1);

            _uow.Commit();

            Assert.AreEqual(2016, turmaEditada.Ano);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Buscar_Todas_Turmas_SQL_Test()
        {
            var turmasEncontradas = _repo.GetAll();

            Assert.IsTrue(turmasEncontradas.Count > 0);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Remover_Turma_SQL_Test()
        {
            var turma = ObjectMother.CreateTurma();

            _repo.Add(turma);

            var qtdTurmas = _repo.GetAll().Count;

            Assert.AreEqual(2, qtdTurmas);

            _repo.Delete(2);

            qtdTurmas = _repo.GetAll().Count;

            _uow.Commit();

            Assert.AreEqual(1, qtdTurmas);
        }
    }
}