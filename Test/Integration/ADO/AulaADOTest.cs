using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.DAO.ORM.Repositories;
using System.Data.Entity;
using Infrasctructure.DAO.ORM.Contexts;
using Domain.Contracts;
using Infrastructure.DAO.Common;
using NDDigital.DiarioAcademia.Infraestrutura.Orm.Common;
using Infrastructure.DAO.SQL.Repositories;

namespace Test
{
    //[TestClass]
    //public class AulaADOTest
    //{

    //    public ITurmaRepository _repoTurma;
    //    public IAulaRepository _repoAula;
    //    public IUnitOfWork _uow;
    //    public IDatabaseFactory _factory;

    //    [TestInitialize]
    //    public void Initialize()
    //    {
    //        new BaseSQLTest();

    //        _factory = new DatabaseFactory();

    //        _uow = new ADOUnitOfWork(_factory);

    //        _repoTurma = new TurmaRepositorySql(_factory)

    //            _repoAula = new AulaRepositorySql(_factory)

    //    }

    //    [TestMethod]
    //    [TestCategory("Teste de Integração Aula")]
    //    public void Deveria_Persistir_Aula_SQL_Test()
    //    {
    //        _repoTurma.Add(ObjectMother.CreateTurma());

    //        var turmaEncontrada = _repoTurma.GetById(1);

    //        var aula = ObjectMother.CreateAula(turmaEncontrada);

    //        aula.Turma = turmaEncontrada;
            
    //        _repoAula.Add(aula);

    //        Assert.IsNotNull(aula);
    //    }

    //    [TestMethod]
    //    [TestCategory("Teste de Integração Aula")]
    //    public void Deveria_Buscar_Aula_SQL_Test()
    //    {
    //        var aulaEncontrada =  _repoAula.GetById(1);

    //        Assert.IsNotNull(aulaEncontrada);
    //        Assert.AreEqual(1, aulaEncontrada.Id);
    //    }

    //    [TestMethod]
    //    [TestCategory("Teste de Integração Aula")]
    //    public void Deveria_Editar_Aula_SQL_Test()
    //    {
    //        var aulaEncontrada = _repoAula.GetById(1);
    //        aulaEncontrada.Data = DateTime.Now.AddYears(-15);

    //        _repoAula.Update(aulaEncontrada);

    //        var aulaEditada = _repoAula.GetById(1);

    //        Assert.AreEqual(2000, aulaEditada.Data.Year);

    //    }

    //    [TestMethod]
    //    [TestCategory("Teste de Integração Aula")]
    //    public void Deveria_Buscar_Todas_Aulas_SQL_Test()
    //    {
    //        var aulasEncontradas = _repoAula.GetAll();

    //        Assert.IsNotNull(aulasEncontradas);
    //    }

    //    [TestMethod]
    //    [TestCategory("Teste de Integração Aula")]
    //    public void Deveria_Remover_Aula_SQL_Test()
    //    {
    //        _repoAula.Delete(1);

    //        var aulasEncontradas = _repoAula.GetAll();

    //        Assert.IsTrue(aulasEncontradas.Count == 0);
    //    }
    //}
}
