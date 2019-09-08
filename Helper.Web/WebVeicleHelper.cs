using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web
{
    public class WebVeicleHelper : IVeicleHelper
    {
        public IVeicleProvider veicleProvider;

        public WebVeicleHelper(IVeicleProvider veicleProvider)
        {
            this.veicleProvider = veicleProvider;
        }

        public Task<ICollection<VeicleAssignement>> AllRequestToValidateasync()
        {
            return veicleProvider.AllRequestToValidateasync();
        }

        public Task<ICollection<VeicleAssignement>> AllValidVeicleAssignement()
        {
            return veicleProvider.AllValidVeicleAssignement();
        }

        public Task<bool> DeleteAsync(string id)
        {
            return veicleProvider.DeleteAsync(id);
        }

        public Task<Veicle> GetById(string id)
        {
            return veicleProvider.GetById(id);
        }

        public Task<Veicle> RetrieveByType(string type)
        {
            return veicleProvider.RetrieveByType(type);
        }

        public Task<Veicle> RetriveByIdAsync(string Id)
        {
            return veicleProvider.RetriveByIdAsync(Id);
        }

        public Task<bool> SaveAsync(Veicle veicle)
        {
            return veicleProvider.SaveAsync(veicle);
        }

        public Task<bool> SaveVeicleAssignement(VeicleAssignement veicleAssignement)
        {
            return veicleProvider.SaveVeicleAssignement(veicleAssignement);
        }

        public Task<bool> ValidateAsync(string id)
        {
            return veicleProvider.ValidateAsync(id);
        }

        public Task<ICollection<VeicleAssignement>> VeicleAssignementsAsync()
        {
            return veicleProvider.VeicleAssignementsAsync();
        }

        public Task<ICollection<VeicleAssignement>> VeicleAssignementsByVeicleId(string id)
        {
            return veicleProvider.VeicleAssignementsByVeicleId(id);
        }

        public Task<ICollection<VeicleAssignement>> VeicleAssignementsValidByVeicleId(string id)
        {
            return veicleProvider.VeicleAssignementsValidByVeicleId(id);
        }

        public async Task<ICollection<Veicle>> VeiclesAsync()
        {
            return await veicleProvider.VeiclesAsync();
        }
        
    }
}
