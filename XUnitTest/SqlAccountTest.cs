using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;

namespace XUnitTest
{

    public class SqlAccountTest : IClassFixture<TestFixture>
    {
        private readonly IAccountProvider accountProvider;
        public SqlAccountTest(TestFixture testFixture)
        {
            this.accountProvider = testFixture.ServiceProvider.GetService<IAccountProvider>();
        }


        [Fact]
        public async Task CheckAccountAsync()
        {
            var accounts = await accountProvider.AccountsAsync();

            Assert.NotEmpty(accounts);
            Assert.IsType<List<Account>>(accounts);
        }

        [Theory]
        [InlineData("gaetano@vetrya.com")]
        public async Task CheckAccountFromEmail(string email)
        {
            //Arrange 
            Account account = await accountProvider.GetByEmailAsync(email);

            //Assert
            Assert.NotNull(account);
            Assert.IsAssignableFrom<Account>(account);

            
        }

        [Fact]
        public async Task CheckAccountFromUser()
        {

            var users = await accountProvider.UsersAsync();
            var user = users.FirstOrDefault();

            var account = await accountProvider.GetByUserAsync(user);

            Assert.IsType<List<Account>>(account);
            

        }

        [Fact]
        public async Task CheckUserList()
        {
            var users = await accountProvider.UsersAsync();

            Assert.NotEmpty(users);
            Assert.IsType<List<User>>(users);
        }
        
        [Fact]
        public async Task CheckGetById()
        {
            var accounts = await accountProvider.AccountsAsync();
            var account = accounts.FirstOrDefault();

            var done = await accountProvider.GetById(account.Id);

            Assert.NotNull(done);
            Assert.IsAssignableFrom<Account>(done);

        }

        [Fact]
        public async Task CheckUserNotPresent()
        {
            var accounts = await accountProvider.AccountsAsync();
            var account = accounts.FirstOrDefault();

            var users = await accountProvider.UsersNotPresentAsync(account.Id);

            
            Assert.IsType<List<User>>(users);

        }

        [Fact]
        public async Task CheckUserById()
        {
            var users = await accountProvider.UsersAsync();
            var user = users.FirstOrDefault();

            var check = await accountProvider.GetUserById(user.Id);

            Assert.NotNull(check);
            Assert.IsType<User>(check);
        }

        [Theory]
        [InlineData("piano", "piano@vetrya.com")]
        public async Task CheckIfAccountIsCreated(string nickname, string email)
        {
            Account account = Account.GetInstanceOf(AccountantTypes.Stakeholder);
            account.Nickname = nickname;
            account.Email = email;

            var created = await accountProvider.CreateAccountAsync(account);

            Assert.NotNull(created);
            Assert.IsAssignableFrom<Account>(created);
            Assert.True(await accountProvider.RemoveAccount(created));
        }

        [Fact]
        public async Task CheckIfAccountIsDeleted()
        {
            var accounts = await accountProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();
            

            var deleted = await accountProvider.DeleteAsync(account.Id);
            var reset = await accountProvider.ResetAccountAsync(account);
            Assert.True(deleted);
            Assert.True(reset);
           
        }

        [Theory]
        [InlineData("a.poldo", "a.poldo@vetrya.com")]
        public async Task CheckIfAccountIsEdit(string nickname, string email)
        {
            var accounts = await accountProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();
            var nick = account.Nickname;
            var mail = account.Email;
            account.Email = email;
            account.Nickname = nickname;

            //Act

            var edit = await accountProvider.EditAsync(account);


            Assert.NotNull(edit);
            Assert.IsAssignableFrom<Account>(edit);
            account.Nickname = nick;
            account.Email = mail;
            await accountProvider.EditAsync(account);


        }

        [Fact]
        public async Task CheckAssignementByAccountId()
        {
            var accounts = await accountProvider.AccountsAsync();
            var account = accounts.FirstOrDefault();

            var assignements = await accountProvider.AssignementsbyAccountIdAsync(account.Id);

            Assert.NotEmpty(assignements);
            Assert.IsType<List<Assignement>>(assignements);
         }

        [Fact]
        public async Task CheckSetAssignementAsync()
        {
            var accounts = await accountProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();
            var users = await accountProvider.UsersAsync();
            User user = users.FirstOrDefault();
            Assignement assignement = new Assignement()
            {
                Account = account,
                User = user,
            };

            var created = await accountProvider.SetAssignementAsync(assignement);

            Assert.NotNull(created);
            Assert.IsType<Assignement>(created);
            Assert.True(await accountProvider.RemoveAssignement(created));
        }

        [Fact]
        public async Task CheckUsersAsync()
        {
            var users = await accountProvider.UsersAsync();

            Assert.NotEmpty(users);
            Assert.IsType<List<User>>(users);
        }

       
    }
}
