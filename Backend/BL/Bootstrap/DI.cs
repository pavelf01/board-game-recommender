using BL.App;
using BL.Repositories;
using BL.Services;
using BL.Services.RecommenderEngine;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL;
using System;
using System.Data.Entity;

namespace BL.Bootstrap
{
    public class DI : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<DbContext>()
                   .Instance(new MainContext())
                   .LifestyleTransient(),
                Classes.FromAssemblyContaining<AppUnitOfWork>()
                    .BasedOn(typeof(BaseRepository<>))
                    .LifestyleTransient(),
                Classes.FromAssemblyContaining<AppUnitOfWork>()
                    .BasedOn(typeof(BaseService<>))
                    .LifestyleTransient(),
                Classes.FromThisAssembly()
                    .InNamespace("BL.Services.RecommenderEngine")
                    .LifestyleTransient()
           );
        }
    }
}
