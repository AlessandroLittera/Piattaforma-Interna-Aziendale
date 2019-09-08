using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace XUnitTestProject
{
    public class SqlRequestTest : IClassFixture<TestFixture>
    {
        private readonly IRequestProvider requestProvider;
        public SqlRequestTest(TestFixture myFixture)
        {
            this.requestProvider = myFixture.ServiceProvider.GetService<IRequestProvider>();
        }

        [Fact]
        public async Task CheckRequestExist()
        {
            //Act

        }
    }
}
