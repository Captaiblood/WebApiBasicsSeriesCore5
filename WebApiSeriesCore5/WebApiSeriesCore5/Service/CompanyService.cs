using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.Dtos.Company;
using WebApiSeriesCore5.Repository.Contract;
using WebApiSeriesCore5.Service.Contract;
using WebApiSeriesCore5.ServiceResponder;

namespace WebApiSeriesCore5.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _compRepo;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._compRepo = companyRepository;
            this._mapper = mapper;
        }
        public async Task<ServiceResponse<CompanyDto>> AddCompanyAsync(CreateCompanyDto createCompanyDto)
        {
            ServiceResponse<CompanyDto> _response = new();
            try
            {


                //Check If company exist
                if (await _compRepo.CompanyExistAsync(createCompanyDto.CompanyName))
                {
                    _response.Message = "Exist";
                    _response.Success = false;
                    _response.Data = null;
                    return _response;

                }

                Entities.Company _newCompany = new()
                {

                    CompanyName = createCompanyDto.CompanyName,
                    GUID = Guid.NewGuid(),
                    CreatedDate = DateTimeOffset.UtcNow,
                    IsEnabled = true,
                    IsDeleted = false
                };

                //Add new record
                if (!await _compRepo.CreateCompanyAsync(_newCompany))
                {
                    _response.Error = "RepoError";
                    _response.Success = false;
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Data = _mapper.Map<CompanyDto>(_newCompany);
                _response.Message = "Created";

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

            }
            return _response;
        }

        public async Task<ServiceResponse<CompanyDto>> GetByGUIDAsync(Guid CompanyGUID)
        {
            ServiceResponse<CompanyDto> _response = new();

            try
            {

                var _Company = await _compRepo.GetCompanyByGUIDAsync(CompanyGUID);

                if (_Company == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _CompanyDto = _mapper.Map<CompanyDto>(_Company);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _CompanyDto;


            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<CompanyDto>> GetByIdAsync(int Id)
        {
            ServiceResponse<CompanyDto> _response = new();

            try
            {


                var _Company = await _compRepo.GetCompanyByIDAsync(Id);

                if (_Company == null)
                {

                    _response.Success = false;
                    _response.Message = "Not Found";
                    return _response;
                }

                var _CompanyDto = _mapper.Map<CompanyDto>(_Company);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _CompanyDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<List<CompanyDto>>> GetCompaniesAsync()
        {
            ServiceResponse<List<CompanyDto>> _response = new();

            try
            {

                var CompaniesList = await _compRepo.GetCompaniesAsync();

                var CompanyListDto = new List<CompanyDto>();              


                foreach (var item in CompaniesList)
                {
                    CompanyListDto.Add(_mapper.Map<CompanyDto>(item));
                }

                //OR 
                //CompanyListDto.AddRange(from item in CompaniesList select _mapper.Map<CompanyDto>(item));
                _response.Success = true;
                _response.Message = "ok";
                _response.Data = CompanyListDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<string>> SoftDeleteCompanyAsync(Guid CompanyGUID)
        {
            ServiceResponse<string> _response = new();

            try
            {
                //check if record exist
                var _existingCompany = await _compRepo.CompanyExistAsync(CompanyGUID);

                if (_existingCompany == false)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;

                }

                if (!await _compRepo.SoftDeleteCompanyAsync(CompanyGUID))
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
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<CompanyDto>> UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto)
        {
            ServiceResponse<CompanyDto> _response = new();

            try
            {
                //check if record exist
                var _existingCompany = await _compRepo.GetCompanyByGUIDAsync(updateCompanyDto.GUID);

                if (_existingCompany == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;

                }

                //Update
                _existingCompany.CompanyName = updateCompanyDto.CompanyName;
                _existingCompany.IsEnabled = updateCompanyDto.IsEnabled;
                _existingCompany.IsDeleted = updateCompanyDto.IsDeleted;

                if (!await _compRepo.UpdateCompanyAsync(_existingCompany))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                //Map updateCompanyDto To Company
                var _companyDto = _mapper.Map<CompanyDto>(_existingCompany);
                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _companyDto;

            }
            catch (Exception ex)
            {

                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
    }
}
