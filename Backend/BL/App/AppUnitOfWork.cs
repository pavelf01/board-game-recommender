using DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Data.Entity;

namespace BL.App
{
    class AppUnitOfWork : EntityFrameworkUnitOfWork
    {
        public new MainContext Context => (MainContext)base.Context;

        public AppUnitOfWork(IUnitOfWorkProvider provider, Func<DbContext> dbContextFactory, DbContextOptions options)
            : base(provider, dbContextFactory, options) { }
    }
}
