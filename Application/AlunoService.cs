using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Application.Services
{
    public interface IAlunoService
    {
        void Add(AlunoDTO alunoDto);

        void Update(AlunoDTO alunoDto);

        void Delete(int id);

        AlunoDTO GetById(int id);

        IList<AlunoDTO> GetAll();

        IList<AlunoDTO> GetAllByTurma(int ano);
    }

    public class AlunoService : IAlunoService
    {
        private IAlunoRepository _alunoRepository;
        private ITurmaRepository _turmaRepository;
        private CepWebService _webService;

        public AlunoService(IAlunoRepository repoAluno, ITurmaRepository repoTurma)
        {
            _alunoRepository = repoAluno;
            _turmaRepository = repoTurma;
        }

        public void Add(AlunoDTO alunoDto)
        {
            Turma turma = _turmaRepository.GetById(alunoDto.TurmaId);

            Aluno aluno = new Aluno(alunoDto.Descricao.Split(':')[0], turma ?? new Turma(2007));//todo: turma vem null

            aluno.Endereco.Bairro = alunoDto.Bairro;
            aluno.Endereco.Cep = alunoDto.Cep;
            aluno.Endereco.Localidade = alunoDto.Localidade;
            aluno.Endereco.Uf = alunoDto.Uf;

            _alunoRepository.Add(aluno);
        }

        public void Update(AlunoDTO alunoDto)
        {
            Turma turma = _turmaRepository.GetById(alunoDto.TurmaId);

            Aluno aluno = _alunoRepository.GetById(alunoDto.Id);

            aluno.Nome = alunoDto.Descricao.Split(':')[0];
            aluno.Turma = turma;
            aluno.Endereco.Bairro = alunoDto.Bairro;
            aluno.Endereco.Cep = alunoDto.Cep;
            aluno.Endereco.Localidade = alunoDto.Localidade;
            aluno.Endereco.Uf = alunoDto.Uf;

            _alunoRepository.Update(aluno);
        }

        public void Delete(int id)
        {
            _alunoRepository.Delete(id);
        }

        public AlunoDTO GetById(int id)
        {
            var aluno = _alunoRepository.GetById(id);

            var alunoDto = new AlunoDTO
            {
                Id = aluno.Id,
                Descricao = aluno.Nome,
                TurmaId = aluno.Turma.Id,
                Cep = aluno.Endereco.Cep,
                Bairro = aluno.Endereco.Bairro,
                Localidade = aluno.Endereco.Localidade,
                Uf = aluno.Endereco.Uf
            };

            if (aluno.Turma != null)
                alunoDto.TurmaId = aluno.Turma.Id;

            return alunoDto;
        }

        public IList<AlunoDTO> GetAll()
        {
            return _alunoRepository.GetAll()
                .Select(aluno => new AlunoDTO(aluno))
                .ToList();
        }

        public IList<AlunoDTO> GetAllByTurma(int ano)
        {
            return _alunoRepository.GetAllByTurma(ano)
              .Select(aluno => new AlunoDTO(aluno))
              .ToList();
        }

        public Endereco BuscaEnderecoPorCep(string cep)
        {
            _webService = new CepWebService();

            return _webService.PreencheEndereco(cep);
        }

        public void GerarRelatorioAlunosPdf(int ano, string path)
        {
            try
            {
                FileStream fs = new FileStream(path,
                           FileMode.Create, FileAccess.Write, FileShare.None);

                Document doc = new Document();

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                doc.Add(new Paragraph("Relatório de presenças - Academia do prgramador " + ano + ":\n\n"));
                doc.Add(new Paragraph("Alunos/Presenças/Faltas:\n\n"));

                foreach (var listaAluno in GetAllByTurma(ano))
                {
                    doc.Add(new Paragraph(listaAluno.Descricao));
                }

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class CepWebService
    {
        private const string WebserviceUrl = "http://viacep.com.br/ws/@cep/@format/";

        public Endereco PreencheEndereco(string cep, string format = "xml")
        {
            var url = WebserviceUrl
                            .Replace("@cep", cep)
                            .Replace("@format", format);
            var xml = XElement.Load(url);

            var endereco = new Endereco();

            endereco.Cep = xml.Element("cep").Value;
            endereco.Bairro = xml.Element("bairro").Value;
            endereco.Localidade = xml.Element("localidade").Value;
            endereco.Uf = xml.Element("uf").Value;

            return endereco;
        }
    }
}