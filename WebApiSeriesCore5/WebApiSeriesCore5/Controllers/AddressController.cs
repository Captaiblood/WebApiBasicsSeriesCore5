using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApiSeriesCore5.ServiceResponder;
using WebApiSeriesCore5.Services.Contract;
using WebApiSeriesCore5.Dto.Address;

namespace WebApiSeriesCore5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IAddressService _addrService;

        public AddressController(IAddressService addressService)
        {
            _addrService = addressService;
        }

        /// <summary>
        /// Get list of all address for a company
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns></returns>
        //Get/{Guid}
        [HttpGet("[action]/{companyGUID:Guid}", Name = "GetCompanyAddressByGUID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCompanyAddress(Guid companyGUID)
        {
            if(companyGUID == Guid.Empty) { return BadRequest(); }

            ServiceResponse<List<AddressDto>> listCompanyAddress = await _addrService.GetCompanyAddressAsync(companyGUID);

            if (listCompanyAddress.Data == null)
            {
                return NotFound();
            }

            return Ok(listCompanyAddress);
        }

        /// <summary>
        /// Get a address by GUID
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns></returns>
        //Get/12
        [HttpGet("{addressGUID:Guid}", Name = "GetAddressByGUID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAddress(Guid addressGUID)
        {
            if (addressGUID == Guid.Empty) { return BadRequest(); }

            ServiceResponse<AddressDto> _address = await _addrService.GetByGUIDAsync(addressGUID);

            if (_address.Data == null)
            {
                return NotFound();
            }

            return Ok(_address);
        }

        /// <summary>
        /// Create a new address for a company
        /// </summary>
        /// <param name="createAddressDto"></param>
        /// <returns></returns>
        //POST/Address
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressDto>> CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
           
            if (createAddressDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newAddress = await _addrService.AddAddressAsync(createAddressDto);

            if (_newAddress.Success == false && _newAddress.Message == "Exist")
            {
                ModelState.AddModelError("", "Address Exist");
                return StatusCode(404, ModelState);
            }

            //only for Demo in production never send response back about internal error.
            if (_newAddress.Success == false && _newAddress.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong at repository layer when adding Address {createAddressDto}");
                return StatusCode(500, ModelState);
            }

            //only for Demo in production never send response back about internal error.
            if (_newAddress.Success == false && _newAddress.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong at service layer when adding Address {createAddressDto}");
                return StatusCode(500, ModelState);
            }

            //Return new address created
            return CreatedAtRoute("GetCompanyAddressByGUID", new { CompanyGUID = _newAddress.Data.CompanyGUID }, _newAddress);

        }

        /// <summary>
        /// Update an existing address
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <param name="updateAddressyDto"></param>
        /// <returns></returns>
        //PUT/Address/{Guid}
        [HttpPut("{addressGUID:Guid}", Name = "UpdateAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddress(Guid addressGUID, [FromBody] UpdateAddressDto updateAddressyDto)
        {
            if (updateAddressyDto == null || updateAddressyDto.GUID != addressGUID)
            {
                return BadRequest(ModelState);
            }


            var _updateAddress = await _addrService.UpdateAddressAsync(updateAddressyDto);

            if (_updateAddress.Success == false && _updateAddress.Message == "NotFound")
            {
                ModelState.AddModelError("", "Address Not found");
                return StatusCode(404, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_updateAddress.Success == false && _updateAddress.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong at repository layer when adding Address {updateAddressyDto}");
                return StatusCode(500, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_updateAddress.Success == false && _updateAddress.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong at service layer when adding Address {updateAddressyDto}");
                return StatusCode(500, ModelState);
            }


            return Ok(_updateAddress);
        }

        /// <summary>
        /// Mark a address as deleted.
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns></returns>
        //DELETE/Address/{Guid} 
        [HttpDelete("{addressGUID:Guid}", Name = "DeleteAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddress(Guid addressGUID)
        {

            var _deleteAddress = await _addrService.SoftDeleteAddressAsync(addressGUID);

            if (_deleteAddress.Success == false && _deleteAddress.Data == "NotFound")
            {
                ModelState.AddModelError("", "Address Not found");
                return StatusCode(404, ModelState);
            }

            //only for Demo in production never send response back about internal error.
            if (_deleteAddress.Success == false && _deleteAddress.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong at Repository layer when deleting Address");
                return StatusCode(500, ModelState);
            }
            //only for Demo in production never send response back about internal error.
            if (_deleteAddress.Success == false && _deleteAddress.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong at service layer when deleting Address");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


    }
}
