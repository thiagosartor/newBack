using Domain.Contracts;
using Infrastructure.DAO.SQL.Common;
using Infrastructure.DAO.SQL.Repositories;
using Ninject;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Infrastructure.IoC
{
    public static class Container
    {
        private static IKernel _container;

        public static T Get<T>()
        {
            return _container.TryGet<T>();
        }

        static Container()
        {
            ConfigContainer();
            //RegisterServices(_container);
        }

        private static void ConfigContainer()
        {
            _container = new StandardKernel();

            string path
                   = new FileInfo(
                                Assembly
                                .GetExecutingAssembly()
                                .Location)
                                .DirectoryName;

            string fileName
                    = ConfigurationSettings.AppSettings["Infrasctructure.DAO"];

            string assemblyFile
                    = string.Format("{0}\\{1}", path, fileName);

            Assembly file = Assembly.LoadFrom(assemblyFile);

            _container.Load(file);
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITurmaRepository>().ToConstructor(f => new TurmaRepositorySql(f.Inject<AdoNetFactory>()));
        }
    }
}