using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAulaRepository : IRepository<Aula>
    {
        Aula GetByData(DateTime data);

        IList<Aula> GetAllByTurma(int ano);
    }
}