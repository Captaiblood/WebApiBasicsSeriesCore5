using WebApiSeriesCore5.Dto.AddressType;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSeriesCore5.ServiceResponder;

namespace WebApiSeriesCore5.Services.Contract
{
    public interface IAddressTypeService
    {
        /// <summary>
        /// Return all addresstypes which are not marked as deleted and enabled
        /// </summary>
        /// <returns>List Of AddressTypeDto</returns>
        Task<ServiceResponse<List<AddressTypeDto>>> GetAddressTypeAsync();
        /// <summary>
        /// Return addresstype.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>AddressTypeDto</returns>
        Task<ServiceResponse<AddressTypeDto>> GetByIdAsync(int Id);
        /// <summary>
        /// Add new addresstype to db.
        /// </summary>
        /// <param name="createAddressTypeDto"></param>
        /// <returns>AddressTypeDto</returns>
        Task<ServiceResponse<AddressTypeDto>> AddAddressAsync(CreateAddressTypeDto createAddressTypeDto);
        /// <summary>
        /// Update addresstype record.
        /// </summary>
        /// <param name="updateAddressTypeDto"></param>
        /// <returns>AddressTypeDto</returns>
        Task<ServiceResponse<AddressTypeDto>> UpdateAddressTypeAsync(UpdateAddressTypeDto updateAddressTypeDto);
        /// <summary>
        /// Marks the addressType record as deleted
        /// </summary>
        /// <param name="addressTypeId"></param>
        /// <returns>bool</returns>
        Task<ServiceResponse<string>> SoftDeleteAddressAsync(int addressTypeId);

    }
}
