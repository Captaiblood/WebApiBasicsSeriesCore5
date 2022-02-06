using AutoMapper;
using WebApiSeriesCore5.Dto.AddressType;
using WebApiSeriesCore5.Repository.Contract;
using WebApiSeriesCore5.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSeriesCore5.Services.Warpper;


namespace WebApiSeriesCore5.Services
{
    public class AddressTypeService : IAddressTypeWrapper
    {
        private readonly IAddressTypeRepository _addrTypeRep;
        private readonly IMapper _mapper;

        public AddressTypeService(IAddressTypeRepository addressTypeRepository, IMapper mapper)
        {
            _addrTypeRep = addressTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// return all addresstypes which are not marked as deleted and enabled.
        /// </summary>
        /// <returns>AddressTypeDto</returns>
        public async Task<ServiceResponse<List<AddressTypeDto>>> GetAddressTypeAsync()
        {

            ServiceResponse<List<AddressTypeDto>> _response = new();
           
            var AddressTypeList = await _addrTypeRep.GetAddressesType();

            var AddressTypeListDto = new List<AddressTypeDto>();

            foreach (var item in AddressTypeList)
            {
                AddressTypeListDto.Add(_mapper.Map<AddressTypeDto>(item));
            }

            _response.Success = true;
            _response.Message = "ok";
            _response.Data = AddressTypeListDto;

            return _response;

        }

        /// <summary>
        /// return addresstype.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>AddressTypeDto</returns>
        public async Task<ServiceResponse<AddressTypeDto>> GetByIdAsync(int Id)
        {

            ServiceResponse<AddressTypeDto> _response = new();

            var _address = await _addrTypeRep.GetAddressTypeByIDAsync(Id);

            if (_address == null)
            {
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

            var _addressTypeDto = _mapper.Map<AddressTypeDto>(_address);

            _response.Success = true;
            _response.Message = "ok";
            _response.Data = _addressTypeDto;

            return _response;

        }

        /// <summary>
        /// Add new addresstype to db
        /// </summary>
        /// <param name="createAddressTypeDto"></param>
        /// <returns>AddressTypeDto</returns>
        public async Task<ServiceResponse<AddressTypeDto>> AddAddressAsync(CreateAddressTypeDto createAddressTypeDto)
        {
            ServiceResponse<AddressTypeDto> _response = new();
            try
            {

                if (await _addrTypeRep.AddressTypeExistAsync(createAddressTypeDto.AddressTypeName))
                {
                    _response.Success = false;
                    _response.Message = "Exist";
                    _response.Data = null;
                    return _response;
                }

                var _newAddressType = new Entities.AddressType();

                _newAddressType.AddressTypeName = createAddressTypeDto.AddressTypeName;
                _newAddressType.IsEnabled = true;
                _newAddressType.IsDeleted = false;

                if (!await _addrTypeRep.CreateAddressTypeAsync(_newAddressType))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Data = _mapper.Map<AddressTypeDto>(_newAddressType);
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
        /// update addresstype record
        /// </summary>
        /// <param name="updateAddressTypeDto"></param>
        /// <returns>AddressTypeDto</returns>
        public async Task<ServiceResponse<AddressTypeDto>> UpdateAddressTypeAsync(UpdateAddressTypeDto updateAddressTypeDto)
        {
            ServiceResponse<AddressTypeDto> _response = new();

            try
            {
                var _exisitngAddressType = await _addrTypeRep.GetAddressTypeByIDAsync(updateAddressTypeDto.Id);

                if (_exisitngAddressType == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                _exisitngAddressType.AddressTypeName = updateAddressTypeDto.AddressTypeName;
                _exisitngAddressType.IsEnabled = updateAddressTypeDto.IsEnabled;
                _exisitngAddressType.IsDeleted = updateAddressTypeDto.IsDeleted;

                if (!await _addrTypeRep.UpdateAddressTypeAsync(_exisitngAddressType))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<AddressTypeDto>(_exisitngAddressType);

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
        /// marks the addressType record as deleted
        /// </summary>
        /// <param name="addressTypeId"></param>
        /// <returns>bool</returns>
        public async Task<ServiceResponse<string>> SoftDeleteAddressAsync(int addressTypeId)
        {
            ServiceResponse<string> _response = new();

            try
            {

                var _existingAddressType = await _addrTypeRep.AddressTypeExistAsync(addressTypeId);

                if (_existingAddressType == false)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                if (!await _addrTypeRep.SoftDeleteAddressTypeAsync(addressTypeId))
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
        /// return all addressTypes including deleted and disbaled
        /// </summary>
        /// <returns>AddressTypeDto</returns>
        async Task<ServiceResponse<List<AddressTypeDto>>> IAddressTypeWrapper.GetAllAddressTypeAsync()
        {
            ServiceResponse<List<AddressTypeDto>> _response = new();

           var CompaniesList = await _addrTypeRep.GetAllAddressesType();
           

            var CompanyListDto = new List<AddressTypeDto>();

            foreach (var item in CompaniesList)
            {
                CompanyListDto.Add(_mapper.Map<AddressTypeDto>(item));
            }

            _response.Success = true;
            _response.Message = "ok";
            _response.Data = CompanyListDto;

            return _response;

        }

        /// <summary>
        /// permanently delete addresstype from DB
        /// </summary>
        /// <param name="addressTypeDto"></param>
        /// <returns>bool</returns>
        async Task<ServiceResponse<string>> IAddressTypeWrapper.HardDeleteAddressTypeAsync(AddressTypeDto addressTypeDto)
        {
            ServiceResponse<string> _response = new();

            try
            {

                var _addressTypeExist = await _addrTypeRep.AddressTypeExistAsync(addressTypeDto.Id);

                if (_addressTypeExist == false)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _existingAddressType = _mapper.Map<Entities.AddressType>(addressTypeDto);

                if (!await _addrTypeRep.HardDeleteAddressTypeAsync(_existingAddressType))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Deleted";

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
        /// Return all deleted addresstype
        /// </summary>
        /// <returns>AddressTypeDto</returns>
        async Task<ServiceResponse<List<AddressTypeDto>>> IAddressTypeWrapper.GetAllDeletedAddressTypeAsync()
        {
            var _response = new ServiceResponse<List<AddressTypeDto>>();

            try
            {
                var _deletedAddressTypeList = await _addrTypeRep.GetDeletedAddressTypesAsync();

                var _addressTypeDtoList = new List<AddressTypeDto>();

                _addressTypeDtoList.AddRange(from items in _deletedAddressTypeList select _mapper.Map<AddressTypeDto>(items));

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _addressTypeDtoList;
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
        /// return all disbaled addresstype
        /// </summary>
        /// <returns>List Of AddressTypeDto</returns>
        async Task<ServiceResponse<List<AddressTypeDto>>> IAddressTypeWrapper.GetAllDisabledAddressTypeAsync()
        {
            var _response = new ServiceResponse<List<AddressTypeDto>>();

            try
            {

                var _disabledAddressTypeList = await _addrTypeRep.GetDisabledAddressTypesAsync();

                var _addressTypeDtoList = new List<AddressTypeDto>();

                _addressTypeDtoList.AddRange(from items in _disabledAddressTypeList select _mapper.Map<AddressTypeDto>(items));

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _addressTypeDtoList;

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
