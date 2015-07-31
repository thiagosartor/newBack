using Application.Services;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Application
{
    [TestClass]
    public class PresencaServiceTests
    {
        private readonly Mock<IAlunoRepository> _alunoRepository = null;
        private readonly Mock<IAulaRepository> _aulaRepository = null;
        private readonly Mock<ITurmaRepository> _turmaRepository = null;

        private IAulaService aulaService = null;

        public PresencaServiceTests()
        {
            _alunoRepository = new Mock<IAlunoRepository>();
            _aulaRepository = new Mock<IAulaRepository>();
            _turmaRepository = new Mock<ITurmaRepository>();

            aulaService = new AulaService(_aulaRepository.Object, _alunoRepository.Object, _turmaRepository.Object);
        }

        [TestMethod]
        [TestCategory("Camada de Serviço ORM")]
        public void RegistraPresenca_deveria_persistir_as_presencas_dos_alunos()
        {
            //arrange
            int qtdAlunos = 5;

            var alunos = ObjectMother.CreateListAlunos(qtdAlunos);

            var ids = new List<int>();

            foreach (var item in alunos)
            {
                ids.Add(item.Id);
            }

            var comando = ObjectMother.CreateRegistraPresencaCommand(ids);

            _alunoRepository
                .Setup(x => x.GetAllByTurmaId(It.IsAny<int>()))
                .Returns(alunos);

            _aulaRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Aula(DateTime.Now, new Turma(2014)));

            //act
            aulaService.RealizaChamada(comando);

            //assert
            _alunoRepository.Verify(x => x.Update(It.IsAny<Aluno>()), Times.Exactly(5));

        }
    }
}