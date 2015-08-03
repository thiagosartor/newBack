using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAO
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
    }
}
