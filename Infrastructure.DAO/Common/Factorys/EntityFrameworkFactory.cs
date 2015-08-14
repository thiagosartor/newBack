using Infrasctructure.DAO.Contexts;
using System;

namespace Infrastructure.DAO.Common
{
    public class EntityFrameworkFactory : UnitOfWorkFactory
    {
        public DiarioAcademiaContext _context;
        public override IUnitOfWork Create()
        {
            _context = new DiarioAcademiaContext();

            return new EFUnitOfWork(_context);
        }
    }
}