using Domain.Entities;
using Infrastructure.DAO.ORM.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Test
{
    [TestClass]
    public class TurmaEFTest
    {
        public TurmaRepositoryEF _repo;

        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(new BaseEFTest()); // Não está funcionando

            new BaseSQLTest();

            _repo = new TurmaRepositoryEF();
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Persistir_Turma_ORM_Test()
        {
            var turma = ObjectMother.CreateTurma();

            _repo.Add(turma);

            var qtdTurmas = _repo.GetAll().Count;

            Assert.AreEqual(2, qtdTurmas);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Buscar_Turma_ORM_Test()
        {
            var turmaEncontrada = _repo.GetById(1);

            Assert.IsNotNull(turmaEncontrada);
            Assert.AreEqual(1, turmaEncontrada.Id);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Editar_Turma_ORM_Test()
        {
            var turmaEncontrada = _repo.GetById(1);
            turmaEncontrada.Ano = 2016;

            _repo.Update(turmaEncontrada);

            var turmaEditada = _repo.GetById(1);

            Assert.AreEqual(2016, turmaEditada.Ano);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Buscar_Todas_Turmas_ORM_Test()
        {
            var turmasEncontradas = _repo.GetAll();

            Assert.AreEqual(1, turmasEncontradas.Count);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Turma")]
        public void Deveria_Remover_Turma_ORM_Test()
        {
            _repo.Add(new Turma());

            var turmasEncontradas = _repo.GetAll();

            Assert.AreEqual(2, turmasEncontradas.Count);

            _repo.Delete(2);

            turmasEncontradas = _repo.GetAll();

            Assert.AreEqual(1,turmasEncontradas.Count);
        }
    }
}