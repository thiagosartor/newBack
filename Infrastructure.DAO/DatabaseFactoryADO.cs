using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.DAO
{

    public interface IDbFactory
    {
        IDiarioAcademiaContext Get();
    }
    public class DatabaseFactoryADO : IDisposable, IDbFactory
    {
        private IDiarioAcademiaContext dataContext;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IDiarioAcademiaContext Get()
        {
            return dataContext ?? (dataContext = new DiarioAcademiaADOContext());
        }
    }
}
