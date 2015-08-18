﻿using Domain.Contracts;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.Common;
using Infrastructure.DAO.Common.Context;
using Infrastructure.DAO.IoC;
using Infrastructure.DAO.ORM.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application
{
    [TestClass]
    public class TurmaServiceTest
    {
        private ITurmaRepository _repo;
        private IUnitOfWork _uow;
        private IDatabaseFactory<EntityFrameworkContext> _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new EntityFrameworkFactory();

            _repo = Container.Get<ITurmaRepository>();

            _uow = new EntityFrameworkUnitOfWork(_factory);

           //_uow = new AdoNetUnitOfWork(_factory);
        }

        [TestMethod]
        public void Test()
        {
        }
    }
}