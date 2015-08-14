using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        IList<Aluno> GetAllByTurmaId(int id);
    }
}