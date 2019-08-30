using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using Models.Contextes;
using System.Linq;
using Models;

namespace XUnitTest
{
   public class SqlTechnologyTest : IClassFixture<TestFixture>
    {
        private readonly ITechnologyProvider technologyProvider;
        private readonly IAccountProvider accountProvider;
        private readonly IPossibleRoleProvider possibleRoleProvider;
        private readonly IRoleProvider roleProvider;
        public SqlTechnologyTest(TestFixture testFixture)
        {
            this.technologyProvider = testFixture.ServiceProvider.GetService<ITechnologyProvider>();
            this.accountProvider = testFixture.ServiceProvider.GetService<IAccountProvider>();
            this.possibleRoleProvider = testFixture.ServiceProvider.GetService<IPossibleRoleProvider>();
            this.roleProvider = testFixture.ServiceProvider.GetService<IRoleProvider>();
        }

        [Fact]
        public async Task CheckTechnologyAsync()
        {
            var technologies = await technologyProvider.TechnologysAsync();

            Assert.NotEmpty(technologies);
            Assert.IsType<List<Technology>>(technologies);
        }

        [Fact]
        public async Task CheckDeleteAsync()
        {
            var technologies = await technologyProvider.TechnologysAsync();
            Technology technology = technologies.FirstOrDefault();


            var deleted = await technologyProvider.DeleteAsync(technology);

            Assert.True(deleted);
            Assert.True(await technologyProvider.ReviveDeletedTechnology(technology.Id));
        }

        [Theory]
        [InlineData("nomeprova")]
        public async Task CheckEditAsync(string newName)
        {
            var technologies = await technologyProvider.TechnologysAsync();
            Technology technology = technologies.FirstOrDefault();
            var oldName = technology.Name;
            technology.Name = newName;

            var edit = await technologyProvider.EditAsync(technology);

            Assert.True(edit.Name == technology.Name);
            Assert.IsType<Technology>(technology);
            technology.Name = oldName;
            await technologyProvider.EditAsync(technology);


        }

        [Theory]
        [InlineData("nomeTech")]
        public async Task CheckCreateTechnologyAsync(string nameTech)
        {
            var accounts = await accountProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();
            Technology technology = new Technology
            {
                Name = nameTech
            };

            var created = await technologyProvider.CreateTechnologyAsync(technology,account);

            Assert.NotNull(created);
            Assert.IsType<Technology>(created);
            Assert.True(await technologyProvider.DeleteCreatedTEchnology(created.Id));
            Assert.True(await possibleRoleProvider.DeletePossibleRole(created.PossibleRoles));
            await technologyProvider.RemoveRoleCreated(technology.Roles);
            
        }

        [Fact]
        public async Task CheckGetByIdAsync()
        {
            var technologies = await technologyProvider.TechnologysAsync();
            Technology technology = technologies.FirstOrDefault();

            var otherTech = await technologyProvider.GetByIdAsync(technology.Id);

            Assert.NotNull(otherTech);
            Assert.IsType<Technology>(otherTech);

   
        }

        [Theory]
        [InlineData( "131", "56", "110")]
        public async Task CheckChangeAdminAsync( string newInCharge, string technologyId, string possibleroleid)
        {
            Account newAccount = await accountProvider.GetById(newInCharge);
            Technology technology = await technologyProvider.GetByIdAsync(technologyId);
            PossibleRole possibleRole = await possibleRoleProvider.GetByIdAsync(possibleroleid);
            Role newRole = new Role { Context = technology, Account = newAccount, DefaultRole = possibleRole };
          


            var changed = await technologyProvider.ChangeAdminAsync(newRole);

            Assert.IsType<Role>(changed);

            Assert.True(await roleProvider.RemoveRoleCreated(changed.Id));
            Assert.True(await roleProvider.ReviveRoleDeleted("147"));

        }
    }
}
