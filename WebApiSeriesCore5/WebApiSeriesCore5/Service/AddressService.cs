using AutoMapper;
using WebApiSeriesCore5.Dto.Address;
using WebApiSeriesCore5.Repository.Contract;
using WebApiSeriesCore5.Services.Contract;
using WebApiSeriesCore5.Services.Warpper;
using WebApiSeriesCore5.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Services
{
    public class AddressService : IAddressWrapper
    {
        private readonly IAddressRespository _addRepo;
        private readonly IMapper _mapper;

        public AddressService(IAddressRespository addressRepository, IMapper mapper)
        {
            _addRepo = addressRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new address record in db
        /// </summary>
        /// <param name="createAddressDto"></param>
        /// <returns>AddressDto</returns>
        async Task<ServiceResponse<AddressDto>> IAddressService.AddAddressAsync(CreateAddressDto createAddressDto)
        {
            var _response = new ServiceResponse<AddressDto>();

            try
            {

                if (await _addRepo.AddressExist(createAddressDto.HouseNoName, createAddressDto.PostCode)) 
                {
                    _response.Success = false;
                    _response.Message = "Exist";
                    _response.Data = null;
                    return _response;
                }

                var _newAddress = new Entities.Address
                {
                    GUID = Guid.NewGuid(),
                    CompanyId = createAddressDto.CompanyId,
                    CompanyGUID = createAddressDto.CompanyGUID,
                    AddressTypeId = createAddressDto.AddressTypeId,
                    HouseNoName = createAddressDto.HouseNoName,
                    Street = createAddressDto.Street,
                    PostCode = createAddressDto.PostCode,
                    City = createAddressDto.City,
                    County = createAddressDto.County,
                    Createddate = DateTimeOffset.UtcNow,
                    IsEnabled = true,
                    IsDeleted = false,

                };

                if (!await _addRepo.CreateAddress(_newAddress))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Data = _mapper.Map<AddressDto>(_newAddress);
                _response.Message = "Created";

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();
            }


            return _response;

        }
        
        /// <summary>
        /// Returns address record
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>AddressDto</returns>
        async Task<ServiceResponse<AddressDto>> IAddressService.GetByGUIDAsync(Guid addressGUID)
        {
            ServiceResponse<AddressDto> _response = new();

            try
            {

                var _address = await _addRepo.GetAddressByGUID(addressGUID);

                if (_address == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                AddressDto addressDto = new();

                addressDto = _mapper.Map<AddressDto>(_address);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = addressDto;

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();
            }

            return _response;

        }
       
        /// <summary>
        /// Reurn address record
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns>AddressDto</returns>
        async Task<ServiceResponse<AddressDto>> IAddressService.GetByIdAsync(int addressId)
        {
            ServiceResponse<AddressDto> _response = new();

            try
            {
                var _address = await _addRepo.GetAddressByID(addressId);
                
                if (_address == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                AddressDto addressDto = new();

                addressDto = _mapper.Map<AddressDto>(_address);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = addressDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();
            }

            return _response;


        }
        
        /// <summary>
        /// Return list of address for a company.
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>List of AddressDto -> (Adresstype, Company)</returns>
        async Task<ServiceResponse<List<AddressDto>>> IAddressService.GetCompanyAddressAsync(Guid companyGUID)
        {
            ServiceResponse<List<AddressDto>> _response = new();

            try
            {
                var _companyAddressList = await _addRepo.GetCompanyAddresses(companyGUID);


                if (_companyAddressList.Count == 0)
                {

                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                List<AddressDto> _companyAddressListDto = new();

                foreach (var item in _companyAddressList)
                {
                    _companyAddressListDto.Add(_mapper.Map<AddressDto>(item));
                }

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _companyAddressListDto;

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();
            }

            return _response;
        }

        /// <summary>
        /// Mark the record address as deleted.
        /// </summary>
        /// <param name="addressGUID"></param>
        /// <returns>bool</returns>
        async Task<ServiceResponse<string>> IAddressService.SoftDeleteAddressAsync(Guid addressGUID)
        {
            ServiceResponse<string> _response = new();

            try
            {

                var _existingAddress = await _addRepo.AddressExist(addressGUID);

                if (_existingAddress == false)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";                    
                    return _response;
                }
                //Further check if record can be marked as deleted if its only address
                //for company.
                if (!await _addRepo.SoftDeleteAddress(addressGUID))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";                   
                    return _response;
                }

                _response.Success = true;
                _response.Message = "SoftDeleted";                

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }

            return _response;

        }

        /// <summary>
        /// Update address.
        /// </summary>
        /// <param name="updateAddressDto"></param>
        /// <returns>AddressDto</returns>
        async Task<ServiceResponse<AddressDto>> IAddressService.UpdateAddressAsync(UpdateAddressDto updateAddressDto)
        {
            ServiceResponse<AddressDto> _response = new();

            try
            {
                var _exisitngAddress = await _addRepo.GetAddressByGUID(updateAddressDto.GUID);

                if (_exisitngAddress == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                _exisitngAddress.HouseNoName = updateAddressDto.HouseNoName;
                _exisitngAddress.Street = updateAddressDto.Street;
                _exisitngAddress.City = updateAddressDto.City;
                _exisitngAddress.County = updateAddressDto.County;
                _exisitngAddress.PostCode = updateAddressDto.PostCode;
                _exisitngAddress.AddressTypeId = updateAddressDto.AddressTypeId;
                _exisitngAddress.IsEnabled = updateAddressDto.IsEnabled;
                _exisitngAddress.IsDeleted = updateAddressDto.IsDeleted;

                if (!await _addRepo.UpdateAddress(_exisitngAddress))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<AddressDto>(_exisitngAddress);

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }

            return _response;
        }


        /// <summary>
        /// Return all address which are marked as deleted and disabled
        /// </summary>
        /// <returns>List Of AddressDto</returns>
        async Task<ServiceResponse<List<AddressDto>>> IAddressWrapper.GetAllAddressAsync()
        {
            ServiceResponse<List<AddressDto>> _response = new();
            try
            {
                var _existingAddresses = await _addRepo.GetAllAddresses();

                var _addressDto = new List<AddressDto>();

                _addressDto.AddRange(from Item in _existingAddresses select _mapper.Map<AddressDto>(Item));

                _response.Success = true;
                _response.Message = "OK";
                _response.Data = _addressDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }
            return _response;
        }
       
        /// <summary>
        /// Return all records marked as deleted
        /// </summary>
        /// <returns>List Of AddressDto</returns>
        async Task<ServiceResponse<List<AddressDto>>> IAddressWrapper.GetAllDeletedAddressAsync()
        {
            ServiceResponse<List<AddressDto>> _response = new();

            try
            {
                var _deletedAddress = await _addRepo.GetDeletedAddresses();

                List<AddressDto> _deltedAddressDto = new();

                _deltedAddressDto.AddRange(from Item in _deletedAddress select _mapper.Map<AddressDto>(Item));

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _deltedAddressDto;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }

            return _response;

        }

        /// <summary>
        /// Return records marked as deleted for a company
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>List Of AddressDto</returns>
        async Task<ServiceResponse<List<AddressDto>>> IAddressWrapper.GetAllDeletedAddressForCompanyAsync(Guid companyGUID)
        {
            ServiceResponse<List<AddressDto>> _response = new();

            try
            {


                var _deletedAddressForCompany = await _addRepo.GetCompanyDeletedAddresses(companyGUID);

                if (_deletedAddressForCompany.Count == 0)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                List<AddressDto> _deletedAddressForCompanyDto = new();

                _deletedAddressForCompanyDto.AddRange(from Item in _deletedAddressForCompany select _mapper.Map<AddressDto>(Item));

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _deletedAddressForCompanyDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }

            return _response;
        }

        /// <summary>
        /// Return all recods marked as disabled
        /// </summary>
        /// <returns>List Of AddressDto</returns>
        async Task<ServiceResponse<List<AddressDto>>> IAddressWrapper.GetAllDisabledAddressAsync()
        {
            ServiceResponse<List<AddressDto>> _response = new();

            try
            {
                var _disabledAddresses = await _addRepo.GetDisabledAddresses();

                List<AddressDto> _disabledAddressesressDto = new();

                _disabledAddressesressDto.AddRange(from Item in _disabledAddresses select _mapper.Map<AddressDto>(Item));

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _disabledAddressesressDto;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }

            return _response;
        }

        /// <summary>
        /// Return records marked as disabled for a company
        /// </summary>
        /// <param name="companyGUID"></param>
        /// <returns>List Of AddressDto</returns>
        async Task<ServiceResponse<List<AddressDto>>> IAddressWrapper.GetAllDisabledAddressForCompanyAsync(Guid companyGUID)
        {
            ServiceResponse<List<AddressDto>> _response = new();

            try
            {
                var _disabledAddressForCompany = await _addRepo.GetCompanyDisabledAddresses(companyGUID);

                if (_disabledAddressForCompany.Count == 0)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                List<AddressDto> _disabledAddressForCompanyDto = new();

                _disabledAddressForCompanyDto.AddRange(from Item in _disabledAddressForCompany select _mapper.Map<AddressDto>(Item));

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _disabledAddressForCompanyDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();

            }

            return _response;
        }

        /// <summary>
        /// Permanently deleted a record from db
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns>bool</returns>
        async Task<ServiceResponse<string>> IAddressWrapper.HardDeleteAddressAsync(AddressDto addressDto)
        {
            ServiceResponse<string> _response = new();
            try
            {

            
            var _addressExist = await _addRepo.AddressExist(addressDto.GUID);

            if (_addressExist == false)
            {
                _response.Message = "NotFound";
                _response.Success = false;
                _response.Data = "Record do not exist";
                return _response;
            }

            var _existingAddress = _mapper.Map<Entities.Address>(addressDto);            

            if (!await _addRepo.HardDeleteAddress(_existingAddress))
            {
                _response.Message = "RepoError";
                _response.Success = false;
                _response.Data = "Some thing went wrong!";
                return _response;
            }

            _response.Message = "Deleted";
            _response.Success = true;
            _response.Data = "Removed Permanentaly";
            }
            catch (Exception ex)
            {
                _response.Success = false;               
                _response.Message = "Error";
                _response.Error = ex.Message.ToString();
            }
            return _response;

           
        }
    }
}
