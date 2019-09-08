using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Models;

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
            var requests = await requestProvider.RequestsAsync();
            var request = requests.FirstOrDefault();

            var exist = await requestProvider.GetById(request.Id);

            Assert.NotNull(exist);
            Assert.IsType<Request>(exist);
        }
        [Fact]
        public async Task CheckRequestByType()
        {
            var requests = await requestProvider.RequestsAsync();
            var request = requests.FirstOrDefault();

            var typed = await requestProvider.RetrieveByType(request.Name);

            Assert.NotNull(typed);
            Assert.Equal(request.Name,typed.Name);
        }
        [Fact]
        public async Task CheckAllValidRequest()
        {
            var requests = await requestProvider.RequestsAsync();
            var request = requests.FirstOrDefault();


            var requestValid = await requestProvider.RequestAssignementsValidByRequestIdAsync(request.Id);

            foreach (var req in requestValid)
            {
                Assert.True(req.IsValid);
            }
        }
    }
}
