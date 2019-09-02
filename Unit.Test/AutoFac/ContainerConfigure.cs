using Autofac;
using Models.Interfaces.Providers;
using Provider.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("Unit.Test.Autofac.ContainerConfigure", "Unit.Test")]
namespace Unit.Test.Autofac
{
   
    public class ContainerConfigure : AutofacTestFramework
    {
        public ContainerConfigure(IMessageSink diagnosticMessageSink)
           : base(diagnosticMessageSink)
        {
        }

     

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<Container>();
            builder.Build();
        }
    }
}
