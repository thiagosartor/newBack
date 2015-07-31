using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class BaseEFTest : DropCreateDatabaseAlways<DiarioAcademiaContext>
    {
        protected override void Seed(DiarioAcademiaContext context)
        {
            base.Seed(context);

            //Adiciona uma turma
            context.Set<Turma>().Add(ObjectMother.CreateTurma());

            //Busca a turma do id = 1
            var turmEncontrada = context.Set<Turma>().Find(1);

            //Adiciona um aluno
            context.Set<Aluno>().Add(ObjectMother.CreateAluno(turmEncontrada));

            //Busca aluno do id = 1
            var alunoEncontrado = context.Set<Aluno>().Find(1);

            //Adiciona uma aula
            context.Set<Aula>().Add(ObjectMother.CreateAula(turmEncontrada));

        }
    }
}
