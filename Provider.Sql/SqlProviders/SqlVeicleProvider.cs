using Models;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Sql.SqlProviders
{
    public class SqlVeicleProvider : IVeicleProvider
    {
        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Veicle> RetriveByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync(Veicle veicle)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Veicle>> VeiclesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
