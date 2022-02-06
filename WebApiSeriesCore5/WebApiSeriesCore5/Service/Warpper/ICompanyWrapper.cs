using WebApiSeriesCore5.Dto;
using WebApiSeriesCore5.Services.Contract;
using WebApiSeriesCore5.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.Dtos.Company;
using WebApiSeriesCore5.Service.Contract;

namespace WebApiSeriesCore5.Services.Warpper
{
    public interface ICompanyWrapper: ICompanyService
    {
        /// <summary>
        /// Return list of all companies including deleted and disabled
        /// </summary>
        /// <returns>List Of CompanyDto</returns>
        public Task<ServiceResponse<List<CompanyDto>>> GetAllCompaniesAsync();
        /// <summary>
        /// Return all records marked deleted
        /// </summary>
        /// <returns>List Of CompanyDto</returns>
        public Task<ServiceResponse<List<CompanyDto>>> GetAllDeletedCompaniesAsync();
        /// <summary>
        /// Return all liquidated records
        /// </summary>
        /// <returns>List Of CompanyDto</returns>
        public Task<ServiceResponse<List<CompanyDto>>> GetGetAllLiquitedCompaniesAsync();
        /// <summary>
        /// Rerun all company records marked as disabled
        /// </summary>
        /// <returns>List Of CompanyDto </returns>
        public Task<ServiceResponse<List<CompanyDto>>> GetAllDisabledCompaniesAsync();
        /// <summary>
        /// Permanently delete company from DB
        /// </summary>
        /// <param name="companyDto"></param>
        /// <returns>Bool</returns>
        public Task<ServiceResponse<string>> HardDeleteCompanyAsync(CompanyDto companyDto);

    }
}
