using Domain.Contracts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.DTOs;
using Infrastructure.DAO.IoC;
using Infrastructure.DAO.Repositories;
using Infrastructure.DAO.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.DAO
{
    [TestClass]
    public class MultipleRepositoryTest
    {
        private ITurmaService _service;
        private readonly ITurmaRepository _turmaRepository = null;
        private readonly Mock<IUnitOfWork> _unitOfWork = null;

        public MultipleRepositoryTest()
        {
            _turmaRepository = Container.Get<ITurmaRepository>();

            //_turmaRepository = new Mock<ITurmaRepository>();

            _unitOfWork = new Mock<IUnitOfWork>();

            _service = new TurmaService(_turmaRepository, _unitOfWork.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void Test()
        {
            var turma = ObjectMother.CreateTurma();

            _service.Add(new TurmaDTO(turma));
        }
    }
}