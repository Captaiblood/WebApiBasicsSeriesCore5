using WebApiSeriesCore5.Data;
using WebApiSeriesCore5.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Repository.Address
{
    public class AddressRepository : IAddressRespository
    {
        private readonly DataContext _DbContext;

        public AddressRepository(DataContext dataContext)
        {
            _DbContext = dataContext;
        }
        public async Task<bool> AddressExist(Entities.Address address)
        {
            return await  _DbContext.Addresses.AnyAsync(AD=>AD.HouseNoName == address.HouseNoName && AD.PostCode== address.PostCode && AD.Street==address.Street);
        }

        public async Task<bool> AddressExist(string HouseNoName, string PostCode)
        {
           var test =  await _DbContext.Addresses.AnyAsync(AD => AD.HouseNoName == HouseNoName && AD.PostCode == PostCode);
            return test;
        }

        public async Task<bool> AddressExist(int Id)
        {
            return await _DbContext.Addresses.AnyAsync(AD => AD.Id == Id);
        }

        public async Task<bool> AddressExist(Guid addressGUID)
        {
            return  await _DbContext.Addresses.AnyAsync(AD => AD.GUID == addressGUID);
        }

        public async Task<bool> CreateAddress(Entities.Address address)
        {
            await _DbContext.Addresses.AddAsync(address);
            return await Save();
        }

        public async Task<ICollection<Entities.Address>> GetAddresses()
        {
            return  await _DbContext.Addresses.Where(AD => AD.IsDeleted == false).ToListAsync();
            
        }

        public async Task<ICollection<Entities.Address>> GetCompanyAddresses(Guid companyGUID)
        {
           
            return  await _DbContext.Addresses.Include(c => c.Company).Include(t=>t.AddressType).Where(a => a.CompanyGUID == companyGUID && a.IsDeleted==false).ToListAsync();
           
        }

        public async Task<ICollection<Entities.Address>> GetAllCompanyAddresses(Guid companyGUID)
        {            
            return await _DbContext.Addresses.Include(c => c.Company).Include(t => t.AddressType).Where(a => a.CompanyGUID == companyGUID).ToListAsync();
        }

        public async Task<Entities.Address> GetAddressByGUID(Guid addressGUID)
        {
            //return await _DbContext.Addresses.SingleOrDefaultAsync(AD => AD.GUID == addressGUID);
            return await _DbContext.Addresses.Include(c => c.Company).Include(t => t.AddressType).SingleOrDefaultAsync(a => a.GUID == addressGUID && a.IsDeleted == false);
        }

        public async Task<Entities.Address> GetAddressByID(int addressId)
        {
            //return await _DbContext.Addresses.SingleOrDefaultAsync(AD => AD.Id == addressId);
            return await _DbContext.Addresses.Include(c => c.Company).Include(t => t.AddressType).SingleOrDefaultAsync(a => a.Id == addressId && a.IsDeleted == false);
        }

        public async Task<ICollection<Entities.Address>> GetAllAddresses()
        {
            //return await _DbContext.Addresses.ToListAsync();
            return await _DbContext.Addresses.Include(c => c.Company).Include(t => t.AddressType).ToListAsync();
        }

        public async Task<ICollection<Entities.Address>> GetDeletedAddresses()
        {
            return await _DbContext.Addresses.Where(AD => AD.IsDeleted == true).ToListAsync();
        }

        public async Task<ICollection<Entities.Address>> GetCompanyDeletedAddresses(Guid companyGUID)
        {
            return await _DbContext.Addresses.Where(AD => AD.IsDeleted == true & AD.CompanyGUID==companyGUID).ToListAsync();
        }

        public async Task<ICollection<Entities.Address>> GetDisabledAddresses()
        {
            return await _DbContext.Addresses.Where(AD => AD.IsEnabled == false).ToListAsync();
        }

        public async Task<ICollection<Entities.Address>> GetCompanyDisabledAddresses(Guid companyGUID)
        {
            return await _DbContext.Addresses.Where(AD => AD.IsEnabled == false && AD.CompanyGUID==companyGUID).ToListAsync();
        }

        public async Task<bool> HardDeleteAddress(Entities.Address address)
        {
             _DbContext.Addresses.Remove(address);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _DbContext.SaveChangesAsync() >= 0 ? true : false;
        }

        

        public async Task<bool> SoftDeleteAddress(Guid addressGUID)
        {
            var _exisitngCompany = await _DbContext.Addresses.SingleOrDefaultAsync(AD => AD.GUID == addressGUID);
            
            _exisitngCompany.IsDeleted = true;
           
            return await Save();
        }

        public async Task<bool> UpdateAddress(Entities.Address address)
        {
            _DbContext.Addresses.Update(address);
            return await Save();
        }
    }
}
