using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Mvc;
using System.Data.Entity;
using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System.Web;
using Generic_Repository_and_Unit_of_Work.Models;
using TipezeNyumbaService.Models;

namespace Generic_Repository_and_Unit_of_Work.Models
{
    public class ContainerBootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        /// <summary>
        /// Registering all the types
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            DbContext entities = new FindAHouseEntities();
            container.RegisterInstance(entities);

            GenericUoW GUoW = new GenericUoW(entities);
            container.RegisterInstance(GUoW);

            MvcUnityContainer.Container = container;
            return container;
        }
    }
}
