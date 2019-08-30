using Models;
using Models.Interfaces.Providers;
using Provider.Sql;
using Provider.Sql.SqlProviders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Autofac;
using Xunit.Frameworks.Autofac;
using Unit.Test.Autofac;

namespace Unit.Test
{
    [UseAutofacTestFramework]
    public class UnitTest1
    {
        private IUserProvider userProvider;
        public UnitTest1(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        [Fact]
        public void Poldo()
        {
            Assert.True(true);
        }
        /// <summary>
        /// Verifichiamo che un utente esiste
        /// </summary>

        [Fact]
        public async Task CheckUserExistance()
        {
           var ass =  typeof(ContainerConfigure).Assembly.GetName().Name;
            // Arrange
            ICollection<User> user = new List<User>();
            //act
            user = await userProvider.UsersAsync();
            var a = userProvider.GetByEmailAsync("a.littera@vetrya.com");
            Assert.NotNull(user);
            //Assert


        }

        /// <summary>
        /// Verifichiamo che un utente NON esiste
        /// </summary>
        [Fact]
        public async Task CheckUserNonExistance()
        {
            User user = new User
            {
                Id = "1459"
            };


            Assert.NotNull(await userProvider.GetAsync(user));
        }

        [Fact]
        public void FalseUser()
        {

            Assert.NotNull(userProvider.UsersAsync());
        }


    }
}
