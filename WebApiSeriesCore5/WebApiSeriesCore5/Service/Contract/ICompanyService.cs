using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.ServiceResponder;

namespace WebApiSeriesCore5.Service.Contract
{
    public interface ICompanyService
    {
        /// <summary>
        /// Return list of companies which are not marked as deleted.
        /// </summary>
        /// <returns>List Of CompanyDto</returns>
        Task<ServiceResponse<List<Dtos.Company.CompanyDto>>> GetCompaniesAsync();
        /// <summary>
        /// Return company record.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>CompanyDto</returns>
        Task<ServiceResponse<Dtos.Company.CompanyDto>> GetByIdAsync(int Id);
        /// <summary>
        /// Return company record.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>CompanyDto</returns>
        Task<ServiceResponse<Dtos.Company.CompanyDto>> GetByGUIDAsync(Guid guid);
        /// <summary>
        /// Add new company record in db
        /// </summary>
        /// <param name="createCompanyDto"></param>
        /// <returns>CompanyDto</returns>
        Task<ServiceResponse<Dtos.Company.CompanyDto>> AddCompanyAsync(Dtos.Company.CreateCompanyDto createCompanyDto);
        /// <summary>
        /// Update company record
        /// </summary>
        /// <param name="updateCompanyDto"></param>
        /// <returns>CompanyDto</returns>
        Task<ServiceResponse<Dtos.Company.CompanyDto>> UpdateCompanyAsync(Dtos.Company.UpdateCompanyDto updateCompanyDto);
        /// <summary>
        /// Marks the company record as deleted
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>bool</returns>
        Task<ServiceResponse<string>> SoftDeleteCompanyAsync(Guid guid);

    }
}
