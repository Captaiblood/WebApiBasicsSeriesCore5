using WebApiSeriesCore5.Dto.Address;
using WebApiSeriesCore5.Services.Contract;
using WebApiSeriesCore5.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Services.Warpper
{
   public interface IAddressWrapper:IAddressService
    {
        /// <summary>
        /// Return all address which are marked as deleted and disabled
        /// </summary>
        /// <returns>List Of AddressDto</returns>
        public Task<ServiceResponse<List<AddressDto>>> GetAllAddressAsync();
        /// <summary>
        /// Return all records marked as deleted
        /// </summary>
        /// <returns>List Of AddressDto</returns>
        public Task<ServiceResponse<List<AddressDto>>> GetAllDeletedAddressAsync();
        /// <summary>
        /// Return all recods marked as disabled
        /// </summary>
        /// <returns>List Of AddressDto</returns>
        public Task<ServiceResponse<List<AddressDto>>> GetAllDisabledAddressAsync();
        /// <summary>
        /// Return records marked as deleted for a company
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>List Of AddressDto</returns>
        public Task<ServiceResponse<List<AddressDto>>> GetAllDeletedAddressForCompanyAsync(Guid companyGUID);
        /// <summary>
        /// Return records marked as disabled for a company
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>List Of AddressDto</returns>
        public Task<ServiceResponse<List<AddressDto>>> GetAllDisabledAddressForCompanyAsync(Guid companyGUID);
        /// <summary>
        /// Permanently deleted a record from db
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns>bool</returns>
        public Task<ServiceResponse<string>> HardDeleteAddressAsync(AddressDto addressDto);
    }
}
