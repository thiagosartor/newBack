using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class AlunoRepositoryEF : IAlunoRepository
    {
        public DiarioAcademiaContext _context;

        public AlunoRepositoryEF()
        {
            _context = new DiarioAcademiaContext();
        }

        public Aluno Add(Aluno entity)
        {
            _context.Alunos.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var alunoRemovido = _context.Alunos.Find(id);
            _context.Alunos.Remove(alunoRemovido);
            _context.SaveChanges();
        }

        public IList<Aluno> GetAll()
        {
            return _context.Alunos.ToList();
        }

        public IList<Aluno> GetAllByTurma(int ano)
        {
            return _context.Alunos.ToList()
                .FindAll(t => t.Turma.Ano == ano);
        }

        public IList<Aluno> GetAllByTurmaId(int id)
        {
            return _context.Alunos.ToList()
                .FindAll(a => a.Turma.Id == id);
        }

        public Aluno GetById(int id)
        {
            return _context.Alunos.Find(id);
        }

        public void Update(Aluno entity)
        {
            _context.SaveChanges();
        }
    }
}