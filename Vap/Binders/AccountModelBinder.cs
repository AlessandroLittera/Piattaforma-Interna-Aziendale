using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Vap.Binders
{
    public class AccountModelBinder : System.Web.Mvc.IModelBinder
    {
        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var discriminator = bindingContext.ValueProvider.GetValue("AccountType").ToString();
            var result = Account.GetInstanceOf(discriminator);
            result.Id = bindingContext.ValueProvider.GetValue("Id").ToString();
            result.Nickname = bindingContext.ValueProvider.GetValue("Nickname").ToString();
            result.Email = bindingContext.ValueProvider.GetValue("Email").ToString();
            //  result.User.Id = bindingContext.ValueProvider.GetValue("User.Id").ToString();
            //bindingContext.ModelState.SetModelValue(
            //        bindingContext.ModelName, );
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }

        //public Task BindModelAsync(ModelBindingContext bindingContext)
        //{
        //    var discriminator = bindingContext.ValueProvider.GetValue("AccountType").ToString();
        //    var result = Account.GetInstanceOf(discriminator);
        //    result.Id = bindingContext.ValueProvider.GetValue("Id").ToString();
        //    result.Nickname = bindingContext.ValueProvider.GetValue("Nickname").ToString();
        //    result.Email = bindingContext.ValueProvider.GetValue("Email").ToString();
        //    //  result.User.Id = bindingContext.ValueProvider.GetValue("User.Id").ToString();
        //    //bindingContext.ModelState.SetModelValue(
        //    //        bindingContext.ModelName, );
        //    bindingContext.Result = ModelBindingResult.Success(result);

        //    return Task.CompletedTask;
        //}

        //private void TryGetValue(Account result, ModelBindingContext bindingContext)
        //{

        //}
    }
}
