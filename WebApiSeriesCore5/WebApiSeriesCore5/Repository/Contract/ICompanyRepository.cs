using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSeriesCore5.Repository.Contract
{
    public interface ICompanyRepository
    {

        /// <summary>
        /// Return all companies including records marked as deleted and disabled
        /// </summary>
        /// <returns>Entites.Company</returns>
        Task<ICollection<Entities.Company>> GetAllCompaniesAsync();
        /// <summary>
        /// Return list of companies which are not marked as deleted.
        /// </summary>
        /// <returns>Entites.Company</returns>
        Task<ICollection<Entities.Company>> GetCompaniesAsync();
        /// <summary>
        /// Return list of companies which are marked as deleted
        /// </summary>
        /// <returns>Entites.Company</returns>
        Task<ICollection<Entities.Company>> GetDeletedCompaniesAsync();
        /// <summary>
        /// Return list of companies which are marked as disabled
        /// </summary>
        /// <returns>Entites.Company</returns>
        Task<ICollection<Entities.Company>> GetDisabledCompaniesAsync();
        /// <summary>
        /// Return a company record
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns>Entites.Company</returns>
        Task<Entities.Company> GetCompanyByIDAsync(int CompanyId);
        /// <summary>
        /// Return a company record
        /// </summary>
        /// <param name="CompanyGUID"></param>
        /// <returns>Entites.Company</returns>
        Task<Entities.Company> GetCompanyByGUIDAsync(Guid CompanyGUID);
        /// <summary>
        /// Return True/False if record exist
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns>bool</returns>
        Task<bool> CompanyExistAsync(string CompanyName);
        /// <summary>
        /// Return True/False if record exist
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>bool</returns>
        Task<bool> CompanyExistAsync(int Id);
        /// <summary>
        /// Return bool if record exist
        /// </summary>
        /// <param name="CompanyGUID"></param>
        /// <returns>bool</returns>
        Task<bool> CompanyExistAsync(Guid CompanyGUID);
        /// <summary>
        /// Add a new record for company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>bool</returns>
        Task<bool> CreateCompanyAsync(Entities.Company company);
        /// <summary>
        /// Update a record in db
        /// </summary>
        /// <param name="company"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateCompanyAsync(Entities.Company company);
        /// <summary>
        /// Update a record as Deleted=True
        /// </summary>
        /// <param name="CompanyGUID"></param>
        /// <returns>bool</returns>
        Task<bool> SoftDeleteCompanyAsync(Guid CompanyGUID);
        /// <summary>
        /// Permanently remove a record from db
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<bool> HardDeleteCompanyAsync(Entities.Company company);

    }
}
