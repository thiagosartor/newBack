﻿using Domain.Contracts;
using Domain.Entities;
using Infrastructure.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Test.IoC
{
    [TestClass]
    public class IoCNinjectEntityFrameworkTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ConfigurationManager.AppSettings["Infrasctructure.DAO"] = "Infrasctructure.DAO.ORM.dll";
        }

        [TestMethod]
        [TestCategory("Teste de IoC")]
        public void Save_Turma_IoC_EF_Test()
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