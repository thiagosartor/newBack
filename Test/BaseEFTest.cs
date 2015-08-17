using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.ORM.Common;
using Infrastructure.DAO.ORM.Repositories;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
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
        public DiarioAcademiaContext _context;
        public IUnitOfWork _uow;
        public IDatabaseFactory _factory;
        public ITurmaRepository _turmaRepository;


        public const string SqlCleanDB = @"DBCC CHECKIDENT ('[TBPresenca]', RESEED, 0)
                                           DBCC CHECKIDENT ('[TBAula]', RESEED, 0)
                                           DBCC CHECKIDENT ('[TBAluno]', RESEED, 0)
                                           DBCC CHECKIDENT ('[TBTurma]', RESEED, 0)

                                           DELETE FROM TBPresenca
                                           DELETE FROM TBAula
                                           DELETE FROM TBAluno
                                           DELETE FROM TBTurma";

        public BaseEFTest()
        {
            _context = new DiarioAcademiaContext();

            _factory = new DatabaseFactory();

            _uow = new EFUnitOfWork(_factory);

            _turmaRepository = new TurmaRepositoryEF(_factory);

            Seed(_context);
        }

        protected override void Seed(DiarioAcademiaContext context)
        {
            context.Database.ExecuteSqlCommand(SqlCleanDB);

            //Adiciona uma turma
            //context.Set<Turma>().Add(ObjectMother.CreateTurma());
            _turmaRepository.Add(ObjectMother.CreateTurma());
   

            //Busca a turma do id = 1
            //var turmEncontrada = context.Set<Turma>().Find(1);

            //Adiciona um aluno
            //context.Set<Aluno>().Add(ObjectMother.CreateAluno(turmEncontrada));

            //Busca aluno do id = 1
            //var alunoEncontrado = context.Set<Aluno>().Find(1);

            //Adiciona uma aula
            //context.Set<Aula>().Add(ObjectMother.CreateAula(turmEncontrada));

            _uow.Commit();
        }
    }
}
