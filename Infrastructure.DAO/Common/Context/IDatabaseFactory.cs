using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAO.Common.Context
{
    public interface IDatabaseFactory<T> : IDisposable
    {
        T Get();
    }
}
