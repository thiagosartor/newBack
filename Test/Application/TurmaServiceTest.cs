using Application.DTOs;
using Application.Services;
using Domain.Contracts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.SQL.Common;
using Infrastructure.DAO.SQL.Repositories;
using Infrastructure.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Application
{
    [TestClass]
    public class TurmaServiceTest
    {
        private ITurmaService _turmaService;
        private ITurmaRepository _repo;
        private IUnitOfWork _uow;

        [TestInitialize]
        public void Initialize()
        {
            var _factory = new AdoNetFactory();//Container.Get<UnitOfWorkFactory>();

            _uow = new ADOUnitOfWork(_factory);//Container.Get<IUnitOfWork>();

            _repo = Container.Get<ITurmaRepository>();//new TurmaRepositorySql(_factory);

            _turmaService = new TurmaService(_repo, _uow);
        }

        [TestMethod]
        public void Test()
        {
            _turmaService.Add(new TurmaDTO(ObjectMother.CreateTurma()));
        }
    }
}