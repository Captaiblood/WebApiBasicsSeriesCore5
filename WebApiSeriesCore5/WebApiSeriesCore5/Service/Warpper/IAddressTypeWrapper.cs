using WebApiSeriesCore5.Dto.AddressType;
using WebApiSeriesCore5.Services.Contract;
using WebApiSeriesCore5.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Services.Warpper
{
    public interface IAddressTypeWrapper : IAddressTypeService
    {
        /// <summary>
        /// Return all addressTypes including deleted and disabled
        /// </summary>
        /// <returns>List Of AddressTypeDto</returns>
        public Task<ServiceResponse<List<AddressTypeDto>>> GetAllAddressTypeAsync();
        /// <summary>
        /// Return all deleted addressType
        /// </summary>
        /// <returns>AddressTypeDto</returns>        
        public Task<ServiceResponse<List<AddressTypeDto>>> GetAllDeletedAddressTypeAsync();
        /// <summary>
        /// Return all disbaled addressType
        /// </summary>
        /// <returns>List Of AddressTypeDto</returns>
        public Task<ServiceResponse<List<AddressTypeDto>>> GetAllDisabledAddressTypeAsync();
        
        /// <summary>
        /// Permanently delete addressType from DB
        /// </summary>
        /// <param name="addressTypeDto"></param>
        /// <returns>bool</returns>
        public Task<ServiceResponse<string>> HardDeleteAddressTypeAsync(AddressTypeDto addressTypeDto);


    }
}
