using Autofac;
using AutoMapper;
using Helper.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using Provider.Sql;
using Provider.Sql.SqlProviders;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Frameworks.Autofac;
using System.Linq;
using Models;
using System.Collections.Generic;
using Vap.Controllers;

namespace XUnitTest
{
    public class SqlUserTest : IClassFixture<TestFixture>
    {
        private readonly IUserProvider userProvider;
        public SqlUserTest(TestFixture myFixture)
        {
            this.userProvider = myFixture.ServiceProvider.GetService<IUserProvider>();
        }

        [Fact]
        public async Task CheckUserExistsAsync()
        {
            // Arrange
            var users = await userProvider.UsersAsync();
            var aUser = users.FirstOrDefault();

            // Act
            aUser = await userProvider.GetAsync(aUser);

            // Assert
            Assert.True(aUser != null);

        }//done

        [Theory]
        [InlineData("0")]
        public async Task CheckUserNotExistAsync(string id)
        {
            try
            {
                var user = await userProvider.GetAsync(new User { Id = id });
                Assert.False(true);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
            }
        }// done

        [Fact]
        public async Task CheckUserByEmail()
        {
            //Arrange
            var accounts = await userProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();

            //Act
            var usersByEmail = await userProvider.GetByEmailAsync(account.Email);

            //Assert
            Assert.IsType<List<User>>(usersByEmail);
        }//done

        [Theory]
        [InlineData("aaaaa")]
        [InlineData("a.cau@mail,it")]
        public async Task CheckUserByEmailNotExist(string falseEmail)
        {
            try
            {
                var user = await userProvider.GetByEmailAsync(falseEmail);
                Assert.True(false);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
            }
        }//done

        [Fact]
        public async Task CheckGetAsync()
        {
            //Arrange
            var users = await userProvider.UsersAsync();
            User user = users.FirstOrDefault();

            //Act
            var aUser = await userProvider.GetAsync(user);

            //Assert
            Assert.NotNull(aUser);
            Assert.IsType<User>(user);

        }
        [Theory]
        [InlineData("0", "ajeje", "brazorf")]
        public async Task GetAsyncNotWork(string id, string name, string surname)
        {
            //Arrange
            User user = new User()
            {
                Id = id,
                Name = name,
                Surname = surname
            };

            //Act
            try
            {
                var aUser = await userProvider.GetAsync(user);
                Assert.True(false);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
            }
        }

       [Fact]
        public async Task CheckUserFromId()
        {
            //Act
            var user = await userProvider.GetById(AccountController.userId);

            //Assert
            Assert.NotNull(user);
            Assert.IsType<User>(user);
        }

        [Fact]
        public async Task CheckAssignementByIdExist()
        {
            //Arrange
            var users = await userProvider.UsersAsync();
            var singleUser = users.FirstOrDefault();
            //Act
            var assignements = await userProvider.AssignementsByUserIdAsync(singleUser.Id);
            //Assert
            Assert.True(assignements != null);
            Assert.IsType<List<Assignement>>(assignements);
        }

        [Theory]
        [InlineData("11")]
        [InlineData("0")]
        [InlineData("x")]
        public async Task CheckAssignementByIdNotExist(string userId)
        {
            try
            {
                var assignement = await userProvider.AssignementsByUserIdAsync(userId);
                Assert.False(true);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
            }
        }
        [Theory]
        [InlineData("lop", "pol")]
        public async Task CheckIfUserIsCreated(string name, string surname)
        {
            //Arrange
            User user = await userProvider.CreateUserAsync(new User { Name = name, Surname = surname });

            //Assert
            Assert.NotNull(user);
            Assert.IsType<User>(user);
            Assert.True(await userProvider.RemoveUser(user));
        }
        
        [Theory]
        [InlineData("prova", "prova")]
        public async Task CheckIfUserIsEdit(string name, string surname)
        {
            //Arrange

            var users = await userProvider.UsersAsync();
            User user = users.FirstOrDefault();
            var originalName = user.Name;
            var originalSurname = user.Surname;
        
            user.Name =name;
            user.Surname = surname;
            //Act
            var created = await userProvider.EditAsync(user);

            //Assert

            Assert.NotNull(created);
            Assert.IsType<User>(created);
            user.Name = originalName;
            user.Surname = originalSurname;
            await userProvider.EditAsync(user);
        }

        [Theory]
        [InlineData("6")]
        public async Task CheckIfUserIsDeleted(string id)
        {
            var user = await userProvider.GetAsync(new User() { Id = id });
            
            var deleted = await userProvider.DeleteAsync(user);
            var reset = await userProvider.ResetUserAsync(user);
         

            Assert.True(deleted);
            Assert.True(reset);
         

        }

        [Fact]
        public async Task CkeckIfAssignamentIsCreated()
        {
            //Arrange
            var users = await userProvider.UsersAsync();
            User user = users.FirstOrDefault();
            var accounts = await userProvider.AccountsAsync();
            Account account = accounts.FirstOrDefault();
            Assignement assignement = new Assignement()
            {
                Account = account,
                User = user
            };

            //Act
            var created = await userProvider.SetAssignementAsync(assignement);

            //Assert

            Assert.NotNull(created);
            Assert.IsType<Assignement>(created);
            Assert.True(await userProvider.RemoveAssignement(created));
        }

        [Fact]
        public async Task CheckIfAssignementIsDeleted()
        {
            //Arrange
            var users = await userProvider.UsersAsync();
            User user = users.FirstOrDefault();
            var assignements = await userProvider.AssignementsByUserIdAsync(user.Id);
            Assignement assignement = assignements.FirstOrDefault();

            //Act
            bool deleted = await userProvider.DeleteAssignement(assignement.Id);
            var reset = await userProvider.ResetAssignementAsync(assignement);
            //Arrange
            Assert.True(deleted);
            Assert.True(reset);
        }
        
        [Fact]
        public async Task CheckListAccounts()
        {
            //Arrange
            var accounts = await userProvider.AccountsAsync();

            // Assert
            Assert.NotEmpty(accounts);
            Assert.IsType<List<Account>>(accounts);
        }

        [Fact]
        public async Task CheckAccountNotPresent()
        {
            var users = await userProvider.UsersAsync();
            User user = users.FirstOrDefault();
            // virificare che non sia nullo

            var accounts = await userProvider.AccountsNotPresentAsync(user.Id);
            

            //Assert
            Assert.IsAssignableFrom<List<Account>>(accounts);
        }

        [Fact]
        public async Task CheckAccountFromId()
        {
            var accounts = await userProvider.AccountsAsync();
            var acc = accounts.FirstOrDefault();


            var account = await userProvider.GetAccountById(acc.Id);

            Assert.NotNull(account);
            Assert.IsAssignableFrom<Account>(account);
           

        }


     

    }
}

    