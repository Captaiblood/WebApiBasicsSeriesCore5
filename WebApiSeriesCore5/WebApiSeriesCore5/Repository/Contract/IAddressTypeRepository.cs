using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.Entities;

namespace WebApiSeriesCore5.Repository.Contract
{
    public interface IAddressTypeRepository
    {
        /// <summary>
        /// Return all addressTypes including records marked as deleted and disabled.
        /// </summary>
        /// <returns>Entities.AddressType</returns>
        Task<ICollection<Entities.AddressType>> GetAllAddressesType();
        /// <summary>
        /// Return all addressTypes not marked as deleted and disabled.
        /// </summary>
        /// <returns>Entities.AddressType</returns>
        Task<ICollection<Entities.AddressType>> GetAddressesType();
        /// <summary>
        /// Return all address type which are marked as deleted
        /// </summary>
        /// <returns>.AddressType</returns>
        Task<ICollection<Entities.AddressType>> GetDeletedAddressTypesAsync();
        /// <summary>
        /// Return all address type which are marked as disabled
        /// </summary>
        /// <returns>AddressType</returns>
        Task<ICollection<Entities.AddressType>> GetDisabledAddressTypesAsync();
        /// <summary>
        /// Return addresstype.
        /// </summary>
        /// <param name="addressTypeId"></param>
        /// <returns>AddressType</returns>
        Task<Entities.AddressType> GetAddressTypeByIDAsync(int addressTypeId);
        /// <summary>
        /// Return bool if record exist
        /// </summary>
        /// <param name="AddressType"></param>
        /// <returns></returns>
        Task<bool> AddressTypeExistAsync(string AddressType);
        /// <summary>
        /// Return a bool if record exist
        /// </summary>
        /// <param name="AddressTypeID"></param>
        /// <returns></returns>
        Task<bool> AddressTypeExistAsync(int AddressTypeID);
        
        /// <summary>
        /// Add a new record in db
        /// </summary>
        /// <param name="AddressType"></param>
        /// <returns></returns>
        Task<bool> CreateAddressTypeAsync(Entities.AddressType AddressType);
        
        /// <summary>
        /// Update the record in db
        /// </summary>
        /// <param name="AddressType"></param>
        /// <returns></returns>
        Task<bool> UpdateAddressTypeAsync(Entities.AddressType AddressType);
        /// <summary>
        /// update the record with IsDeleted=True.
        /// </summary>
        /// <param name="AddressTypeID"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAddressTypeAsync(int AddressTypeID);
        /// <summary>
        /// Permanentaly remove record from db.
        /// </summary>
        /// <param name="AddressType"></param>
        /// <returns></returns>
        Task<bool> HardDeleteAddressTypeAsync(Entities.AddressType AddressType);
    }
}
