using Application.DTOs;
using Application.Services;
using Domain.Contracts;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Factorys;
using Infrastructure.DAO.IoC;
using Infrastructure.DAO.ORM.Common;
using Infrastructure.DAO.SQL.Common;
using Infrastructure.DAO.SQL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;

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
            var _factory = new AdoNetFactory();

            _uow = new ADOUnitOfWork(_factory);

            _repo = Container.Get<ITurmaRepository>();

            _turmaService = new TurmaService(_repo, _uow);
        }

        [TestMethod]
        public void Test()
        {
            _turmaService.Add(new TurmaDTO(ObjectMother.CreateTurma()));
        }
    }
}