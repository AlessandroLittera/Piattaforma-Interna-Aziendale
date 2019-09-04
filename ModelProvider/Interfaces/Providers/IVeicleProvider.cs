using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
    public interface IVeicleProvider
    {
        Task<ICollection<Veicle>> VeiclesAsync();
        Task<Veicle> RetriveByIdAsync(string Id);
        Task<bool> SaveAsync(Veicle veicle);
        Task<bool> DeleteAsync(string id);

    }
}
