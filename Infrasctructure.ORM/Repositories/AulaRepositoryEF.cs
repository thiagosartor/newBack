using Domain.Contracts;
using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class AulaRepositoryEF : IAulaRepository
    {
        public DiarioAcademiaContext _context;

        public AulaRepositoryEF()
        {
            _context = new DiarioAcademiaContext();
        }

        public Aula Add(Aula entity)
        {
            _context.Aulas.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var alunoRemovido = _context.Aulas.Find(id);
            _context.Aulas.Remove(alunoRemovido);
            _context.SaveChanges();
        }

        public IList<Aula> GetAll()
        {
            return _context.Aulas.ToList();
        }

        public IList<Aula> GetAllByTurma(int ano)
        {
            return _context.Aulas.ToList()
                .FindAll(a => a.Turma.Ano == ano);
        }

        public Aula GetByData(DateTime data)
        {
            return _context.Aulas.ToList().Find(a => a.Data == data);
        }

        public Aula GetById(int id)
        {
            return _context.Aulas.Find(id);
        }

        public void Update(Aula entity)
        {
            _context.SaveChanges();
        }
    }
}