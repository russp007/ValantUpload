using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ValantExercise.IoC;
using ValantExercise.Managers;
using ValantExercise.Output;
using ValantExercise.Repositories;

namespace ValantExercise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // init the IoC
            var container = new UnityContainer();
            container.RegisterInstance<IInventoryRepository>(new InventoryRepository());
            container.RegisterType<IInventoryManager, InventoryManager>();
            container.RegisterType<INotificationManager, NotificationManager>();
            container.RegisterType<INotificationWriter, NotificationWriter>();
            config.DependencyResolver = new UnityResolver(container);            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
