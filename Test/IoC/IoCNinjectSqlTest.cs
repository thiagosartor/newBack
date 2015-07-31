using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using Domain.Contracts;
using Infrastructure.DAO;

namespace Test.IoC
{
    [TestClass]
    public class IoCNinjectSqlTest
    {
        [TestMethod]
        [TestCategory("Teste de IoC")]
        public void Save_Turma_IoC_SQL_Test()
        {

            Turma t = ObjectMother.CreateTurma();

            //Através do IoC Ninject busca um implementação do Repositório
            ITurmaRepository repository
                = Container.Get<ITurmaRepository>();

            Turma turmaAdcionada = repository.Add(t);

            Assert.IsTrue(turmaAdcionada.Id > 0);
        }
    }
}
