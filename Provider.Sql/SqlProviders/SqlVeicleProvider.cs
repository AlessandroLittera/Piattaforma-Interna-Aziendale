using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Sql.SqlProviders
{
    public class SqlVeicleProvider : IVeicleProvider
    {

        private SqlModelsContext dbContext;
        private IMapper mapper;
        public SqlVeicleProvider(SqlModelsContext sqlModelsContext,IMapper mapper)
        {
            this.dbContext = sqlModelsContext;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            if (int.TryParse(id, out int veicleId))
            {
                SqlVeicle sqlVeicle = dbContext.SqlVeicles.FirstOrDefault(x=>x.Id == veicleId);
                if (sqlVeicle == null)
                {
                    return false;
                }
                dbContext.Remove(sqlVeicle);
                return await dbContext.SaveChangesAsync()>0;
            }
            return false;
        }

        public async Task<Veicle> RetriveByIdAsync(string Id)
        {
            if(int.TryParse(Id,out int veicleId))
            {
                SqlVeicle sqlVeicle = dbContext.SqlVeicles.FirstOrDefault(x => x.Id == veicleId);
                if (sqlVeicle == null)
                {
                    return null;
                }
                return mapper.Map<Veicle>(sqlVeicle);
            }
            return null;
        }

        public async Task<bool> SaveAsync(Veicle veicle)
        {
            if (veicle != null)
            {
                SqlVeicle sqlVeicle = mapper.Map<SqlVeicle>(veicle);
                dbContext.SqlVeicles.Add(sqlVeicle);
                return await dbContext.SaveChangesAsync() > 0;
                
            }
            return false;
        }

        public async Task<ICollection<VeicleAssignement>> VeicleAssignementsByVeicleId(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int veicleAssignementId))
            {
                List<SqlVeicleAssignement> sqlVeicleAssignement =  dbContext.SqlVeicleAssignements.Where(x=>x.Id == veicleAssignementId).ToList();
                return mapper.Map<List<VeicleAssignement>>(sqlVeicleAssignement);
            }
            return null;
        }

        public async Task<ICollection<VeicleAssignement>> VeicleAssignementsValidByVeicleId(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int veicleAssignementId))
            {
                List<SqlVeicleAssignement> sqlVeicleAssignement = dbContext.SqlVeicleAssignements.Where(x => x.Id == veicleAssignementId).Where(x=>x.IsValid).ToList();
                return mapper.Map<List<VeicleAssignement>>(sqlVeicleAssignement);
            }
            return null;
        }

        public async Task<ICollection<Veicle>> VeiclesAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Veicle>>(dbContext.SqlVeicles);
        }
        public async Task<bool> SaveVeicleAssignement(VeicleAssignement veicleAssignement)
        {
            if (veicleAssignement == null)
            {
                if (int.TryParse(veicleAssignement.Account.Id,out int accountId) & int.TryParse(veicleAssignement.Veicle.Id,out int veicleId))
                {
                    SqlAccount sqlAccount = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
                    SqlVeicle sqlVeicle = await dbContext.SqlVeicles.FirstOrDefaultAsync(x => x.Id == veicleId);
                    SqlVeicleAssignement sqlVeicleAssignement = mapper.Map<SqlVeicleAssignement>(veicleAssignement);
                    sqlVeicleAssignement.SqlAccount = sqlAccount;
                    sqlVeicleAssignement.SqlVeicle = sqlVeicle;

                    await dbContext.SqlVeicleAssignements.AddAsync(sqlVeicleAssignement);

                    return await dbContext.SaveChangesAsync() > 0;
                }
                
            }
            return false;
        }
        public async Task<bool> ValidateAsync(string id)
        {
            if (int.TryParse(id, out int veicleAssignementId))
            {
                SqlVeicleAssignement sqlVeicleAssignement = await dbContext.SqlVeicleAssignements.FirstOrDefaultAsync(x=>x.Id == veicleAssignementId);
                sqlVeicleAssignement.IsValid = true;
                return await dbContext.SaveChangesAsync()>0;
            }
            return false;
        }
        public async Task<ICollection<VeicleAssignement>> AllValidVeicleAssignement()
        {
            await Task.Delay(0);
            return mapper.Map<List<VeicleAssignement>>(dbContext.SqlVeicleAssignements.Where(x=>x.IsValid).ToList());
        }
    }
}
