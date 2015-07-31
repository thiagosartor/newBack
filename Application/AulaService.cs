using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public interface IAulaService
    {
        void Add(AulaDTO aulaDto);

        void RealizaChamada(ChamadaDTO registroPresencaDto);

        ChamadaDTO GetChamadaByAula(AulaDTO aula);

        void Update(AulaDTO aulaDto);

        void Delete(int id);

        AulaDTO GetById(int id);

        IList<AulaDTO> GetAllByTurma(int anoTurma);
    }

    public class AulaService : IAulaService
    {
        private IAulaRepository _aulaRepository;
        private IAlunoRepository _alunoRepository;
        private ITurmaRepository _turmaRepository;

        private const string NENHUM_ALUNO_ENCOTRADO_PARA_TURMA = "Nenhum aluno encontrado para a turma de {0}";
        private const string NENHUMA_AULA_ENCOTRADA_NESTA_DATA = "Nenhuma aula encontrada para esta data {0}";

        public AulaService(IAulaRepository repoAula, IAlunoRepository repoAluno, ITurmaRepository repoTurma)
        {
            _aulaRepository = repoAula;
            _alunoRepository = repoAluno;
            _turmaRepository = repoTurma;
        }

        public void Add(AulaDTO aulaDto)
        {
            Turma turma = _turmaRepository.GetById(aulaDto.TurmaId);

            Aula aula = new Aula(aulaDto.DataAula, turma);

            _aulaRepository.Add(aula);
        }

        public void RealizaChamada(ChamadaDTO registroPresenca)
        {
            var alunos = _alunoRepository.GetAllByTurmaId(registroPresenca.TurmaId);

            if (alunos == null || alunos.Any() == false)
                throw new Exception(String.Format(NENHUM_ALUNO_ENCOTRADO_PARA_TURMA, registroPresenca.AnoTurma));

            var aula = _aulaRepository.GetById(registroPresenca.AulaId);

            if (aula == null)
                throw new Exception(String.Format(NENHUMA_AULA_ENCOTRADA_NESTA_DATA, registroPresenca.Data));

            foreach (var item in registroPresenca.Alunos)
            {
                var aluno = alunos.First(x => x.Id == item.AlunoId);

                aluno.RegistraPresenca(aula, item.Status);

                _alunoRepository.Update(aluno);
            }

            aula.ChamadaRealizada = true;

            _aulaRepository.Update(aula);
        }

        public void Update(AulaDTO aulaDto)
        {
            Aula aula = _aulaRepository.GetById(aulaDto.Id);

            aula.Data = aulaDto.DataAula;

            _aulaRepository.Update(aula);
        }

        public void Delete(int id)
        {
            _aulaRepository.Delete(id);
        }

        public AulaDTO GetById(int id)
        {
            var aula = _aulaRepository.GetById(id);

            return new AulaDTO
            {
                Id = aula.Id,
                DataAula = aula.Data,
                TurmaId = aula.Turma.Id               
            };
        }

        public IList<AulaDTO> GetAllByTurma(int ano)
        {
            return _aulaRepository
                .GetAllByTurma(ano)
                .Select(aula => new AulaDTO(aula))
                .ToList();
        }

        public ChamadaDTO GetChamadaByAula(AulaDTO aulaDTO)
        {
            var chamada = new ChamadaDTO();
            chamada.AnoTurma = aulaDTO.AnoTurma;
            chamada.Data = aulaDTO.DataAula;

            Aula aula = _aulaRepository.GetById(aulaDTO.Id);

            if (aula.ChamadaRealizada)
            {
                chamada.Alunos = aula.Presencas.
                    Select(x => new ChamadaAlunoDTO(x.Aluno.Id, x.Aluno.Nome, x.StatusPresenca))
                    .ToList();
            }
            else
            {
                var alunos = _alunoRepository.GetAllByTurmaId(aulaDTO.TurmaId);

                chamada.Alunos = alunos.Select(x => new ChamadaAlunoDTO(x.Id, x.Nome, "C"))
                   .ToList();
            }

            return chamada;
        }

        public ChamadaDTO GetChamadaByAula(int id)
        {
            return GetChamadaByAula(GetById(id));
        }

        public IList<AulaDTO> GetAll()
        {
            return _aulaRepository.GetAll()
                .Select(aula => new AulaDTO(aula))
                .ToList();
        }
    }
}