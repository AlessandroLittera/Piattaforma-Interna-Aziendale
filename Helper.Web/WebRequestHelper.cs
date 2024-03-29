﻿using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web.Contextes
{
    public class WebRequestHelper : IRequestHelper
    {
        private IRequestProvider provider;
        public WebRequestHelper(IRequestProvider provider)
        {
            this.provider = provider;
        }

        public Task<ICollection<Request>> RequestsAsync()
        {
            return provider.RequestsAsync();
        }

        public Task<Request> CreateRequestAsync(Request request)
        {
            return provider.CreateRequestAsync(request);
        }

        public Task<bool> DeleteAsync(Request request)
        {
            return provider.DeleteAsync(request);
        }

        public Task<Request> EditAsync(Request request)
        {
            return provider.EditAsync(request);
        }

        public Task<ICollection<Request>> RequestByAccountIdAsync(string accountId)
        {
            return provider.RequestByAccountIdAsync(accountId);
        }

        public Task<ICollection<RequestAssignement>> RequestAssignementsValidByRequestIdAsync(string id)
        {
            return provider.RequestAssignementsValidByRequestIdAsync(id);
        }

        public Task<ICollection<RequestAssignement>> RequestAssignementsByRequestIdAsync(string id)
        {
            return provider.RequestAssignementsByRequestIdAsync(id);
        }

        public Task<bool> SaveRequestAssignement(RequestAssignement requestAssignement)
        {
            return provider.SaveRequestAssignementAsync(requestAssignement);
        }

        public Task<Request> RetrieveByType(string type)
        {
            return provider.RetrieveByType(type);
        }

        public Task<Request> GetById(string id)
        {
            return provider.GetById(id);
        }

        public Task<ICollection<RequestAssignement>> RequestAssignementsAsync()
        {
            return provider.RequestAssignementsAsync();
        }

        public Task<ICollection<RequestAssignement>> RequestAssignementsToValidateAsync()
        {
            return provider.RequestAssignementsToValidateAsync();
        }
    }
}
