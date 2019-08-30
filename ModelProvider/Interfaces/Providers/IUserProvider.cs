using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
    public interface IUserProvider
    {
        /// <summary>
        /// this method return a list of useres that are able 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<User>> UsersAsync();
        /// <summary>
        /// this method set deactivation date of an user that is passed on param, all ralation(assignement) are a deactivation date  setted
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(User user);
        /// <summary>
        /// this method allow do edit user field (name, surname etc)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> EditAsync(User user);
        /// <summary>
        /// this method allow to create new user, after the creation, create account is invoked and default account'll create
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> CreateUserAsync(User user);
        /// <summary>
        /// this method return the user passed on params 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> GetAsync(User user);
        /// <summary>
        /// this method return a collection of user that have a realtion with a specific account identified with its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ICollection<User>> GetByEmailAsync(string email);
        /// <summary>
        /// this method set a new relation between user and account that are included in assignement
        /// </summary>
        /// <param name="assignement"></param>
        /// <returns></returns>
        Task<Assignement> SetAssignementAsync(Assignement assignement);
        /// <summary>
        /// this method return a collection of relations that an user have with accounts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ICollection<Assignement>> AssignementsByUserIdAsync(string id);
        /// <summary>
        /// this method set deactivation date of a specific relation passed on params
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAssignement(string id);
        /// <summary>
        /// this method return a collecton of all account present in db
        /// </summary>
        /// <returns></returns>
        Task<ICollection<Account>> AccountsAsync();
        /// <summary>
        /// this method return a collection of account that don't have a relation with an user identified with his id passed on param
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ICollection<Account>> AccountsNotPresentAsync(string id);
        /// <summary>
        /// this method return an user identified with his id passed on param
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetById(string id);
        /// <summary>
        /// return an account identified with its id passed on param
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Account> GetAccountById(string id);

        // for unitTest
        Task<bool> RemoveUser(User user);
        Task<bool> RemoveAccount(Account account);
        Task<bool> RemoveAssignement(Assignement assignement);
        Task<bool> ResetUserAsync(User user);
        Task<bool> ResetAccountAsync(Account account);
        Task<bool> ResetAssignementAsync(Assignement assignement);
    }
}
