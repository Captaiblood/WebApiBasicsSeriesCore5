using WebApiSeriesCore5.Dto.Address;
using WebApiSeriesCore5.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Services.Contract
{
   public interface IAddressService
    {
        /// <summary>
        /// Return list of address for a company.
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>List of AddressDto -> (Adresstype, Company)</returns>
        public Task<ServiceResponse<List<AddressDto>>> GetCompanyAddressAsync(Guid companyGUID);
        /// <summary>
        /// Reurn address record
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns>AddressDto</returns>
        public Task<ServiceResponse<AddressDto>> GetByIdAsync(int addressId);
        /// <summary>
        /// Returns address record
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>AddressDto</returns>
        public Task<ServiceResponse<AddressDto>> GetByGUIDAsync(Guid addressGUID);
        /// <summary>
        /// Add new address record in db
        /// </summary>
        /// <param name="createAddressDto"></param>
        /// <returns>AddressDto</returns>
        public Task<ServiceResponse<AddressDto>> AddAddressAsync(CreateAddressDto createAddressDto);
        /// <summary>
        /// Update address.
        /// </summary>
        /// <param name="updateAddressDto"></param>
        /// <returns>AddressDto</returns>
        public Task<ServiceResponse<AddressDto>> UpdateAddressAsync(UpdateAddressDto updateAddressDto);
        /// <summary>
        /// Mark the address as deleted.
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>bool</returns>
        public Task<ServiceResponse<string>> SoftDeleteAddressAsync(Guid addressGUID);

    }
}
