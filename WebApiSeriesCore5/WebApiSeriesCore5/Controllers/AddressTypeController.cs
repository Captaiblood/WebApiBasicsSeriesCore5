using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiSeriesCore5.Dto.AddressType;
using WebApiSeriesCore5.Services.Contract;

namespace WebApiSeriesCore5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypeController : ControllerBase
    {
        private readonly IAddressTypeService _addrTypeService;

        public AddressTypeController(IAddressTypeService addressTypeService)
        {
            _addrTypeService = addressTypeService;
        }
        /// <summary>
        /// Get all the Address Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AddressTypeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var _addressTypes = await _addrTypeService.GetAddressTypeAsync();

            return Ok(_addressTypes);
        }

        /// <summary>
        /// Get Address Type by Id.
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        //GET/companies/123
        [HttpGet("{addressId:int}", Name = "GetAddressById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AddressTypeDto>> GetById(int addressId)
        {
            if(addressId <= 0) { return BadRequest(); }

            var _addressType = await _addrTypeService.GetByIdAsync(addressId);

            if (_addressType == null)
            {
                return NotFound();
            }

            return Ok(_addressType);
        }

        /// <summary>
        /// Create new Address Type.
        /// </summary>
        /// <param name="createAddressTypeDto"></param>
        /// <returns></returns>
        //POST/AddressType
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressTypeDto>> CreateAddress([FromBody] CreateAddressTypeDto createAddressTypeDto)
        {
            if (createAddressTypeDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newAddressType = await _addrTypeService.AddAddressAsync(createAddressTypeDto);

            if (_newAddressType.Success == false && _newAddressType.Message == "Exist")
            {
                ModelState.AddModelError("", "Address type Exist");
                return StatusCode(404, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_newAddressType.Success == false && _newAddressType.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong at repository layer when adding address type {createAddressTypeDto}");
                return StatusCode(500, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_newAddressType.Success == false && _newAddressType.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong at service layer when adding address type {createAddressTypeDto}");
                return StatusCode(500, ModelState);
            }

            //Return new address created
            return CreatedAtRoute("GetAddressById", new { addressId = _newAddressType.Data.Id }, _newAddressType);

        }

        /// <summary>
        /// Update an Address Type.
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="updateAddressTypeDto"></param>
        /// <returns></returns>
        //PUT/AddressType/{Id}
        [HttpPut("{addressId:int}", Name = "UpdateAddressType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddressType(int addressId, [FromBody] UpdateAddressTypeDto updateAddressTypeDto)
        {
            if (updateAddressTypeDto == null || updateAddressTypeDto.Id != addressId)
            {
                return BadRequest(ModelState);
            }


            var _updateAddress = await _addrTypeService.UpdateAddressTypeAsync(updateAddressTypeDto);

            if (_updateAddress.Success == false && _updateAddress.Message == "NotFound")
            {
                ModelState.AddModelError("", "AddressType Not found");
                return StatusCode(404, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_updateAddress.Success == false && _updateAddress.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong at repository layer when adding AddressType {updateAddressTypeDto}");
                return StatusCode(500, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_updateAddress.Success == false && _updateAddress.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong at service layer when adding AddressType {updateAddressTypeDto}");
                return StatusCode(500, ModelState);
            }


            return Ok(_updateAddress);
        }

        /// <summary>
        /// Mark a Address Type as deleted.
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        //DELETE/Address/{Guid} 
        [HttpDelete("{addressId:int}", Name = "DeleteAddressType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompany(int addressId)
        {

            var _deleteCompany = await _addrTypeService.SoftDeleteAddressAsync(addressId);

            if (_deleteCompany.Success == false && _deleteCompany.Data == "NotFound")
            {
                ModelState.AddModelError("", "AddressType Not found");
                return StatusCode(404, ModelState);
            }

            //only for Demo in production never send response back about internal error.
            if (_deleteCompany.Success == false && _deleteCompany.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository layer when deleting AddressType");
                return StatusCode(500, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_deleteCompany.Success == false && _deleteCompany.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong at service layer when deleting AddressType");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
