using WebApiSeriesCore5.Data;
using WebApiSeriesCore5.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Repository.AddressType
{
    public class AdressTypeRepository : IAddressTypeRepository
    {
        private readonly DataContext dataContext;

        public AdressTypeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<bool> AddressTypeExistAsync(string AddressType)
        {
            return await dataContext.AddressTypes.AnyAsync(at => at.AddressTypeName==AddressType);
        }

        public async Task<bool> AddressTypeExistAsync(int AddressTypeID)
        {
            return await dataContext.AddressTypes.AnyAsync(at => at.Id == AddressTypeID);
        }

        public async Task<bool> CreateAddressTypeAsync(Entities.AddressType AddressType)
        {
            await dataContext.AddressTypes.AddAsync(AddressType);
            return await Save();
        }

        public async Task<ICollection<Entities.AddressType>> GetAllAddressesType()
        {
            return await dataContext.AddressTypes.ToListAsync();
        }

        public async Task<ICollection<Entities.AddressType>> GetAddressesType()
        {
            return await dataContext.AddressTypes.Where(AT=>AT.IsDeleted==false && AT.IsEnabled==true).ToListAsync();
        }

        public async Task<ICollection<Entities.AddressType>> GetDeletedAddressTypesAsync()
        {
            return await dataContext.AddressTypes.Where(AT => AT.IsDeleted == true).ToListAsync();
        }

        public async Task<ICollection<Entities.AddressType>> GetDisabledAddressTypesAsync()
        {
            return await dataContext.AddressTypes.Where(AT => AT.IsEnabled == false).ToListAsync();
        }

        public async Task<Entities.AddressType> GetAddressTypeByIDAsync(int addressTypeId) 
        {
            return await dataContext.AddressTypes.SingleOrDefaultAsync(AT => AT.Id == addressTypeId);

        }

        public async Task<bool> HardDeleteAddressTypeAsync(Entities.AddressType AddressType)
        {
            dataContext.AddressTypes.Remove(AddressType);
           return  await Save();
        }

        public async Task<bool> SoftDeleteAddressTypeAsync(int AddressTypeID)
        {
            var _exisitngAddressType = await dataContext.AddressTypes.SingleOrDefaultAsync(AT => AT.Id == AddressTypeID);

            _exisitngAddressType.IsDeleted = true;

            return await Save();
        }

        public async  Task<bool> UpdateAddressTypeAsync(Entities.AddressType AddressType)
        {
            dataContext.AddressTypes.Update(AddressType);
            return  await Save();
        }

        private async Task<bool> Save()
        {
            return await dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
