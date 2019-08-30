using Autofac;
using Models;
using Models.Interfaces.Providers;
using Provider.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unit.Test.Autofac
{
   public  class Container: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlUserProvider>().As<IUserProvider>();
            
        }

        //public  static void  ContainerBuilder()
        //{
        //    var containerBuilder = new ContainerBuilder();
            
        //    containerBuilder.RegisterType<SqlUserProvider>().As<IUserProvider>();
        //    var container = containerBuilder.Build();
            
        //}
    }
}
