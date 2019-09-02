using Models.Contextes;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace XUnitTest
{
    public class SqlAreaTest : IClassFixture<TestFixture>
    {
        private readonly IAreaProvider areaProvider;
        private readonly IAccountProvider accountProvider;
        private readonly IPossibleRoleProvider possibleRoleProvider;
        private readonly IRoleProvider roleProvider;
        public SqlAreaTest(TestFixture myFixture)
        {
            this.areaProvider = myFixture.ServiceProvider.GetService<IAreaProvider>();
            this.accountProvider = myFixture.ServiceProvider.GetService<IAccountProvider>();
            this.possibleRoleProvider = myFixture.ServiceProvider.GetService<IPossibleRoleProvider>();
            this.roleProvider = myFixture.ServiceProvider.GetService<IRoleProvider>();
        }

        [Fact]
        public async Task CheckifAreaAreReturn()
        {
            var areas = await areaProvider.AreasAsync();
            Area area = areas.FirstOrDefault();

            Assert.NotNull(area);
            Assert.NotEmpty(areas);
            Assert.IsType<Area>(area);

        }
        [Fact]
        public async Task CheckifAreAllAtDomain()
        {
            var domainAccounts = await areaProvider.ListDomainAccountsAsync();
            Account account = domainAccounts.FirstOrDefault();
            string domain = "vetrya.com";

            Assert.NotEmpty(domainAccounts);
            Assert.Contains(domain, account.Email);

        }

        [Theory]
        [InlineData("nomearea")]
        public async Task CheckIfAreaisCreated(string nomerArea)
        {
            var accounts = await accountProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();

            Area area = new Area
            {
                Name = nomerArea,

            };

            var created = await areaProvider.CreateAreaAsync(area, account);

            Assert.NotNull(created);
            Assert.IsType<Area>(created);
            await possibleRoleProvider.DeletePossibleRole(created.PossibleRoles);
            await areaProvider.DeleteCreatedArea(created.Id);
            await areaProvider.RemoveRoleCreated(created.Roles);

        }

        [Fact]
        public async Task CheckIfAreaisDeleted()
        {
            var areas = await areaProvider.AreasAsync();
            Area area = areas.FirstOrDefault();

            bool deleted = await areaProvider.DeleteAsync(area);

            Assert.True(deleted);
            await areaProvider.ReviveDeletedArea(area.Id);
        }

        [Fact]
        public async Task CheckListAccounts()
        {
            var accounts = await areaProvider.AccountsAsync();

            Assert.NotEmpty(accounts);
            Assert.IsAssignableFrom<List<Account>>(accounts);

        }

        [Theory]
        [InlineData("1")]
        public async Task CheckGetByIdAsync(string id)
        {
            var areaFromDb = await areaProvider.GetByIdAsync(id);

            Assert.NotNull(areaFromDb);
            Assert.IsType<Area>(areaFromDb);
        }

        [Fact]
        public async Task CheckListDomainAsync()
        {
            var domainAccounts = await areaProvider.ListDomainAccountsAsync();

            Assert.NotEmpty(domainAccounts);
            Assert.IsAssignableFrom<List<Account>>(domainAccounts);

        }

        [Theory]
        [InlineData("61", "166", "1", "102")]
        public async Task CheckChangeInchargeAsync(string oldInCharge, string newInCharge, string areaId, string possibleroleid)
        {
            Account newAccount = await accountProvider.GetById(newInCharge);
            Account oldAccount = await accountProvider.GetById(oldInCharge);
            Area area = await areaProvider.GetByIdAsync(areaId);
            PossibleRole possibleRole = await possibleRoleProvider.GetByIdAsync(possibleroleid);
            Role newRole = new Role { Context = area, Account = newAccount, DefaultRole = possibleRole };
          


            var changed = await areaProvider.ChangeInChargeAsync(newRole);

            Assert.IsType<Role>(changed);
            Assert.True(await roleProvider.RemoveRoleCreated(changed.Id));
            Assert.True(await roleProvider.ReviveRoleDeleted("136"));
          


        }

        [Fact]
        public async Task CheckRoleFromArea()
        {
            var areas = await areaProvider.AreasAsync();
            Area area = areas.FirstOrDefault();

            var roles = await areaProvider.RolesFromArea(area);

            Assert.NotEmpty(roles);
            Assert.IsType<List<Role>>(roles);
        }

        [Fact]
        public async Task CheckAccountNotPresent()
        {
            var areas = await areaProvider.AreasAsync();
            Area area = areas.FirstOrDefault();
            string areaId = area.Id.ToString();

            var accountNotPresent = await areaProvider.AccountNotPresentAsync(areaId);

            Assert.NotEmpty(accountNotPresent);
            Assert.IsAssignableFrom<List<Account>>(accountNotPresent);
        }

        [Theory]
        [InlineData("67")]
        public async Task CheckAddAccountAsync(string newAccountId)
        {
            var roles = await roleProvider.RolesAsync();
            Role oldRole = roles.FirstOrDefault();
            Role newRole = new Role()
            {
                Context = oldRole.Context,
                DefaultRole = oldRole.DefaultRole
            };
            Account account = await accountProvider.GetById(newAccountId);
            newRole.Account = account;

            var added = await areaProvider.AddAccountAsync(newRole);

            Assert.NotNull(added);
            Assert.IsType<Role>(added);
            Assert.True(await roleProvider.RemoveRoleCreated(added.Id));

        }

        [Theory]
        [InlineData("137")]
        public async Task CheckRemoveAccountOnAreaAsync(string roleId)
        {
            var roles = await roleProvider.RolesAsync();
            Role role = roles.FirstOrDefault(x => x.Id == roleId);

            var removed = await areaProvider.RemoveAccountOnArea(roleId);

            Assert.True(removed);
            Assert.True(await roleProvider.ReviveRoleDeleted(role.Id));
        }
    }
}
