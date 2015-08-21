using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Factorys;
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
    public class BaseEFTest : DropCreateDatabaseAlways<EntityFrameworkContext> // Não está funcionando
    {
        public EntityFrameworkContext _context;
        public IUnitOfWork _uow;
        public EntityFrameworkFactory _factory;
        public ITurmaRepository _turmaRepository;
        public IAulaRepository _aulaRepository;
        public IAlunoRepository _alunoRepository;


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
            _context = new EntityFrameworkContext();

            _factory = new EntityFrameworkFactory();

            _uow = new EntityFrameworkUnitOfWork(_factory);

            _turmaRepository = new TurmaRepositoryEF(_factory);
            _aulaRepository = new AulaRepositoryEF(_factory);
            _alunoRepository = new AlunoRepositoryEF(_factory);


            Seed(_context);
        }

        protected override void Seed(EntityFrameworkContext context)
        {
            context.Database.ExecuteSqlCommand(SqlCleanDB);

            //Adiciona uma turma
            _turmaRepository.Add(ObjectMother.CreateTurma());   

            //Busca a turma do id = 1
            var turmEncontrada = _turmaRepository.GetById(1);

            //Adiciona um aluno
            _alunoRepository.Add(ObjectMother.CreateAluno(turmEncontrada));

            //Adiciona uma aula
            _aulaRepository.Add(ObjectMother.CreateAula(turmEncontrada));

            _uow.Commit();
        }
    }
}
