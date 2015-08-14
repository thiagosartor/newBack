using Infrastructure.DAO.Common;
using Infrastructure.DAO.ORM.Common;
using Infrastructure.DAO.ORM.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Test
{
    [TestClass]
    public class AlunoEFTest
    {
        public AlunoRepositoryEF _repoAluno;
        public TurmaRepositoryEF _repoTurma;
        public EntityFrameworkFactory _uowFactory;
        public IUnitOfWork _uow;

        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(new BaseEFTest());

            _uowFactory = new EntityFrameworkFactory();

           // _uowFactory.Create();

            _uow = new EFUnitOfWork();

            _repoAluno = new AlunoRepositoryEF();
            _repoTurma = new TurmaRepositoryEF();
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Persistir_Aluno_ORM_Test()
        {
            _repoTurma.Add(ObjectMother.CreateTurma());

            _uow.Commit();

            var turmaEncontrada = _repoTurma.GetById(2);

            var aluno = ObjectMother.CreateAluno(turmaEncontrada);

            aluno.Turma = turmaEncontrada;

            _repoAluno.Add(aluno);

            _uow.Commit();

            Assert.IsNotNull(aluno);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Buscar_Aluno_ORM_Test()
        {
            var alunoEncontrado = _repoAluno.GetById(1);

            Assert.IsNotNull(alunoEncontrado);
            Assert.AreEqual(1, alunoEncontrado.Id);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Editar_Aluno_ORM_Test()
        {
            var alunoEncontrado = _repoAluno.GetById(1);
            alunoEncontrado.Nome = "Alexandre Regis";

            _repoAluno.Update(alunoEncontrado);

            _uow.Commit();

            var alunoEditada = _repoAluno.GetById(1);

            Assert.AreEqual("Alexandre Regis", alunoEditada.Nome);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Buscar_Todas_Alunos_ORM_Test()
        {
            var alunosEncontrados = _repoAluno.GetAll();

            Assert.IsNotNull(alunosEncontrados);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Remover_Aluno_ORM_Test()
        {
            _repoAluno.Delete(1);

            _uow.Commit();

            var alunosEncontrados = _repoAluno.GetAll();

            Assert.IsTrue(alunosEncontrados.Count == 0);
        }

        [TestMethod]
        [TestCategory("Teste de Integração Aluno")]
        public void Deveria_Buscar_Alunos_Por_TurmaId_ORM_Test()
        {
            var alunos = _repoAluno.GetAllByTurmaId(0);

            Assert.IsTrue(alunos.Count > 0);
        }
    }
}