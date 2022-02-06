using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Repository.Contract
{
    public interface IAddressRespository
    {
        /// <summary>
        /// Return ICollection of addresses including records marked as deleted and disabled.
        /// </summary>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetAllAddresses();
        /// <summary>
        /// Return ICollection of addresses including records marked as disabled.
        /// </summary>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetAddresses();
        /// <summary>
        /// Return ICollection of addresses marked as deleted.
        /// </summary>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetDeletedAddresses();
        /// <summary>
        /// Return ICollection of addresses marked as deleted for a company.
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetCompanyDeletedAddresses(Guid companyGUID);
        /// <summary>
        /// Return ICollection of addresses marked as disabled including marked as deleted.
        /// </summary>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetDisabledAddresses();
        /// <summary>
        /// Return ICollection of addresses marked as disabled including marked as deleted for a company.
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetCompanyDisabledAddresses(Guid companyGUID);
        /// <summary>
        /// Return ICollection of addresses including marked as disabled but not marked as deleted for a company.
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetCompanyAddresses(Guid companyGUID);
        /// <summary>
        /// Return ICollection of addresses including marked as disabled and marked as deleted for a company.
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>Entites.Address</returns>
        Task<ICollection<Entities.Address>> GetAllCompanyAddresses(Guid companyGUID);
        /// <summary>
        /// Return address including marked as disabled and marked as deleted.
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns>Entites.Address</returns>
        Task<Entities.Address> GetAddressByID(int addressId);
        /// <summary>
        /// Return address including marked as disabled and marked as deleted.
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>Entites.Address</returns>
        Task<Entities.Address> GetAddressByGUID(Guid addressGUID);
        /// <summary>
        /// Return bool if address record exist including marked as disabled and marked as deleted.
        /// it will compare HouseNoName - PostCode -Street.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>bool</returns>
        Task<bool> AddressExist(Entities.Address address);
        /// <summary>
        /// Return bool if address record exist including marked as disabled and marked as deleted.
        /// it will compare HouseNoName - PostCode.
        /// </summary>
        /// <param name="HouseNoName"></param>
        /// <param name="PostCode"></param>
        /// <returns>bool</returns>
        Task<bool> AddressExist(string HouseNoName, string PostCode);
        /// <summary>
        /// Return bool if address record exist including marked as disabled and marked as deleted.        
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>bool</returns>
        Task<bool> AddressExist(int Id);
        /// <summary>
        /// Return bool if address record exist including marked as disabled and marked as deleted.       
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>bool</returns>
        Task<bool> AddressExist(Guid addressGUID);
        /// <summary>
        /// Add new record in db      
        /// </summary>
        /// <param name="address"></param>
        /// <returns>bool</returns>
        Task<bool> CreateAddress(Entities.Address address);
        /// <summary>
        /// Update a record in db      
        /// </summary>
        /// <param name="address"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAddress(Entities.Address address);
        /// <summary>
        /// update a record IsDeleted=True in db      
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>bool</returns>
        Task<bool> SoftDeleteAddress(Guid addressGUID);
        /// <summary>
        /// Permapentaly remove a record in db      
        /// </summary>
        /// <param name="address"></param>
        /// <returns>bool</returns>
        Task<bool> HardDeleteAddress(Entities.Address address);


    }
}
