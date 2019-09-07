using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IVeicleHelper
    {
        Task<ICollection<Veicle>> VeiclesAsync();
        Task<Veicle> RetriveByIdAsync(string Id);
        Task<bool> SaveAsync(Veicle veicle);
        Task<bool> DeleteAsync(string id);
        Task<ICollection<VeicleAssignement>> VeicleAssignementsValidByVeicleId(string id);
        Task<ICollection<VeicleAssignement>> VeicleAssignementsByVeicleId(string id);
        Task<bool> SaveVeicleAssignement(VeicleAssignement veicleAssignement);
        Task<bool> ValidateAsync(string id);
        Task<ICollection<VeicleAssignement>> AllValidVeicleAssignement();
    }
}
