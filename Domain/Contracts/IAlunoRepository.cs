using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        IList<Aluno> GetAllByTurma(int ano);

        IList<Aluno> GetAllByTurmaId(int id);
    }
}