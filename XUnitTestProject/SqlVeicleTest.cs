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
    public class SqlVeicleTest: IClassFixture<TestFixture>
    {
        private readonly IVeicleProvider veicleProvider;
        public  SqlVeicleTest(TestFixture myFixture)
        {
            this.veicleProvider = myFixture.ServiceProvider.GetService<IVeicleProvider>();
        }

        [Fact]
        public async Task CheckVeicleExist()
        {
            //Act
            var veicles = await veicleProvider.VeiclesAsync();
            var veicle = veicles.FirstOrDefault();

            var exist = await veicleProvider.GetById(veicle.Id);

            Assert.NotNull(exist);
            Assert.IsType<Veicle>(exist);
        }
        [Fact]
        public async Task CheckVeicleByType()
        {
            var veicles = await veicleProvider.VeiclesAsync();
            var veicle = veicles.FirstOrDefault();

            var typed = await veicleProvider.RetrieveByType(veicle.Name);

            Assert.NotNull(typed);
            Assert.Equal(veicle.Name, typed.Name);
        }
        [Fact]
        public async Task CheckAllValidVeicleRequest()
        {
            var veicles = await veicleProvider.VeiclesAsync();
            var veicle = veicles.FirstOrDefault();


            var veicleValid = await veicleProvider.VeicleAssignementsValidByVeicleId(veicle.Id);

            foreach (var v in veicleValid)
            {
                Assert.True(v.IsValid);
            }
        }
    }
}
