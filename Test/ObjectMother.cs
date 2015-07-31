using Application.DTOs;
using Domain.Entities;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;

namespace Test
{
    public static class ObjectMother
    {
        public static Turma CreateTurma()
        {
            return Builder<Turma>.CreateNew()
            .WithConstructor(() =>
            new Turma(2014)).Build();
        }

        public static Aluno CreateAluno(Turma turma)
        {
            return Builder<Aluno>.CreateNew()
            .WithConstructor(() =>
            new Aluno("Thiago Sartor", turma)).Build();
        }

        private static Endereco CreateEndereco()
        {
            return Builder<Endereco>.CreateNew()
            .WithConstructor(() =>
            new Endereco("88509720",
            "Ferrovia", 
            "Lages", 
            "SC")).Build();
        }

        public static Aula CreateAula(Turma turma)
        {
            return Builder<Aula>.CreateNew()
            .WithConstructor(() =>
            new Aula(DateTime.Now, turma)).Build();
        }

        public static ChamadaDTO CreateRegistraPresencaCommand(IList<int> ids)
        {
            var comando = Builder<ChamadaDTO>.CreateNew()
                .With(x => x.AnoTurma = 2014)
                .With(x => x.Data = new DateTime(2000, 10, 10))
                .Build();

            foreach (var id in ids)
            {
                comando.Alunos.Add(new ChamadaAlunoDTO { AlunoId = id, Nome = "Nome"+id, Status = "C" });
            }

            return comando;
        }
        public static IList<Aluno> CreateListAlunos(int qtdAlunos)
        {
            return Builder<Aluno>
                .CreateListOfSize(qtdAlunos)
                .All().With(
                    x => x.Presencas = new List<Presenca>())
                .Build();
        }
    }
}