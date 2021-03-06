﻿using BL.Bootstrap;
using Castle.Windsor;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Data.Entity;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container = new WindsorContainer();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BootstrapContainer();
        }
        public IWindsorContainer Container
        {
            get { return container; }
        }

        private void BootstrapContainer()
        {
            container.Install(new WebApiInstaller());
            container.Install(new DI());

            GlobalConfiguration.Configuration.Services
                .Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);   
        }
    }
}
