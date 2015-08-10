using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DAO.Repositories
{
    public class TurmaRepositoryEF : ITurmaRepository
    {
        public DiarioAcademiaContext _context;

        public TurmaRepositoryEF()
        {
            _context = new DiarioAcademiaContext();
        }

        public Turma Add(Turma entity)
        {
            _context.Turmas.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var turmaRemovida = GetById(id);
            if (turmaRemovida != null)
            {
                _context.Turmas.Remove(turmaRemovida);

                _context.SaveChanges();
            }
        }

        public IList<Turma> GetAll()
        {
            return _context.Turmas.ToList();
        }

        public Turma GetById(int id)
        {
            return _context.Turmas.Find(id);
        }

        public void Update(Turma entity)
        {
            _context.SaveChanges();
        }
    }
}