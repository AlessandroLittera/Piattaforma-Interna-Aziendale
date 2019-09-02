using Models;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Models.Contextes;
using System.Linq;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace XUnitTest
{
   public class SqlRoleTest: IClassFixture<TestFixture>
    {
        private readonly IRoleProvider roleProvider;
        private readonly IAccountProvider accountProvider;
        private readonly IPossibleRoleProvider possibleRoleProvider;
        private readonly IAreaProvider areaProvider;
        public SqlRoleTest(TestFixture testFixture)
        {
            this.roleProvider = testFixture.ServiceProvider.GetService<IRoleProvider>();
            this.accountProvider = testFixture.ServiceProvider.GetService<IAccountProvider>();
            this.possibleRoleProvider = testFixture.ServiceProvider.GetService<IPossibleRoleProvider>();
            this.areaProvider = testFixture.ServiceProvider.GetService<IAreaProvider>();
        }

        [Fact]
        public async Task CheckRoleAsync()
        {
            var roles = await roleProvider.RolesAsync();

            Assert.NotEmpty(roles);
            Assert.IsType<List<Role>>(roles);
        }

        [Theory]
        [InlineData("1","61","103")]
        public async Task CheckCreateRoleAsync(string areaId,string accountId,string possibleRoleId)
        {
            Role role = new Role();
            Account account = await accountProvider.GetById(accountId);
            Area area = await areaProvider.GetByIdAsync(areaId);
            PossibleRole possibleRole = await possibleRoleProvider.GetByIdAsync(possibleRoleId);
            role.Account = account;
            role.DefaultRole = possibleRole;
            role.Context = area;

            var created = await roleProvider.CreateRoleAsync(role);

            Assert.NotNull(created);
            Assert.IsType<Role>(created);
            Assert.True(await roleProvider.RemoveRoleCreated(created.Id));
        }

        [Fact]
        public async Task CheckDeleteAsync()
        {
            var roles = await roleProvider.RolesAsync();
            Role role = roles.FirstOrDefault();

            bool deleted = await roleProvider.DeleteAsync(role);


            Assert.True(deleted);
            Assert.True(await roleProvider.ReviveRoleDeleted(role.Id));
        }

        [Theory]
        [InlineData("131", "54", "104")]
        public async Task CheckChangeAdmingeAsync(string newInCharge, string areaId, string possibleroleid)
        {
            Account newAccount = await accountProvider.GetById(newInCharge);
            Area area = await areaProvider.GetByIdAsync(areaId);
            PossibleRole possibleRole = await possibleRoleProvider.GetByIdAsync(possibleroleid);
            Role newRole = new Role { Context = area, Account = newAccount, DefaultRole = possibleRole };

            var changed = await roleProvider.ChangeInChargeAccountsAsync(newRole);

            Assert.IsType<Role>(changed);

            Assert.True(await roleProvider.RemoveRoleCreated(changed.Id));
            Assert.True(await roleProvider.ReviveRoleDeleted("139"));

        }

        [Fact]
        public async Task CheckRoleFromContextAsync()
        {
            //var context = new Area()
            //{
            //    Id = "79"
            //};

            var areas = await areaProvider.AreasAsync();
            Context context = areas.FirstOrDefault(x => x.Roles.Any(y => y.DeactivationDate == null));

            var roles = await roleProvider.RoleFromContextAsync(context);

            Assert.NotEmpty(roles);
            Assert.IsType<List<Role>>(roles);
        }
        [Fact]
        public async Task CheckAccountNotPresent()
        {
            var areas = await areaProvider.AreasAsync();
            Area area = areas.FirstOrDefault();
            string areaId = area.Id.ToString();

            var accountNotPresent = await roleProvider.AccountNotPresentAsync(areaId);

            Assert.NotEmpty(accountNotPresent);
            Assert.IsAssignableFrom<List<Account>>(accountNotPresent);
        }
    }
}
