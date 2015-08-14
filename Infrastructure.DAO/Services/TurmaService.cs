
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DAO.Services
{
    public interface ITurmaService
    {
        void Add(TurmaDTO turmaDto);

        void Delete(int id);

        IList<TurmaDTO> GetAll();

        TurmaDTO GetById(int id);

        void Update(TurmaDTO turmaDto);
    }

    public class TurmaService : ITurmaService
    {
        private IUnitOfWork _unitOfWork;
        private ITurmaRepository _turmaRepository;
        private UnitOfWorkFactory _uowFactory;

        public TurmaService(ITurmaRepository repoTurma, IUnitOfWork unitOfWork)
        {
            _turmaRepository = repoTurma;
            _unitOfWork = unitOfWork;
             _uowFactory = new AdoNetFactory();
            //_uowFactory = new EntityFrameworkFactory();
        }

        public void Add(TurmaDTO turmaDto)
        {
            Turma turma = new Turma(turmaDto.Ano);

            using (var uow = _uowFactory.Create())
            {
                _turmaRepository.Add(turma);               

                uow.Commit();
            }
         
        }

        public void Update(TurmaDTO turmaDto)
        {
            Turma turma = _turmaRepository.GetById(turmaDto.Id);

            turma.Ano = turmaDto.Ano;

            _turmaRepository.Update(turma);

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _turmaRepository.Delete(id);

            _unitOfWork.Commit();
        }

        public TurmaDTO GetById(int id)
        {
            var turma = _turmaRepository.GetById(id);

            return new TurmaDTO
            {
                Id = turma.Id,
                Ano = turma.Ano
            };
        }

        public IList<TurmaDTO> GetAll()
        {
            return _turmaRepository
                .GetAll()
                .Select(turma => new TurmaDTO(turma))
                .ToList();
        }
    }
}