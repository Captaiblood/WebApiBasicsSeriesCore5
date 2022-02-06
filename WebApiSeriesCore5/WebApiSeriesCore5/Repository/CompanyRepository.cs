using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSeriesCore5.Data;
using WebApiSeriesCore5.Entities;
using WebApiSeriesCore5.Repository.Contract;

namespace WebApiSeriesCore5.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;

        public CompanyRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CompanyExistAsync(string CompanyName)
        {
            return await _dataContext.Companies.AnyAsync(Comp => Comp.CompanyName.Contains(CompanyName));
        }

        public async Task<bool> CompanyExistAsync(int Id)
        {
            return await _dataContext.Companies.AnyAsync(Comp => Comp.Id == Id);
        }

        public Task<bool> CompanyExistAsync(Guid CompanyGUID)
        {
            return _dataContext.Companies.AnyAsync(Comp => Comp.GUID == CompanyGUID);
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            await _dataContext.Companies.AddAsync(company);
            return await Save();
        }
        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            _dataContext.Companies.Update(company);
            return await Save();
        }

        public async Task<ICollection<Company>> GetAllCompaniesAsync()
        {
            return await _dataContext.Companies.ToListAsync();
        }

        public async Task<ICollection<Company>> GetCompaniesAsync()
        {
            return await _dataContext.Companies.Where(Comp => Comp.IsDeleted == false).ToListAsync();
        }

        public async Task<Company> GetCompanyByGUIDAsync(Guid CompanyGUID)
        {
            return await _dataContext.Companies.FirstOrDefaultAsync(Comp => Comp.GUID == CompanyGUID);
        }

        public async Task<Company> GetCompanyByIDAsync(int CompanyId)
        {
            return await _dataContext.Companies.FirstOrDefaultAsync(Comp => Comp.Id == CompanyId);
        }

        public async Task<ICollection<Company>> GetDeletedCompaniesAsync()
        {
            return await _dataContext.Companies.Where(Comp => Comp.IsDeleted == true).ToListAsync();
        }

        public async Task<ICollection<Company>> GetDisabledCompaniesAsync()
        {
            return await _dataContext.Companies.Where(Comp => Comp.IsEnabled == false).ToListAsync();
        }

        public async Task<bool> HardDeleteCompanyAsync(Company company)
        {
            _dataContext.Remove(company);
            return await Save();
        }

        public async Task<bool> SoftDeleteCompanyAsync(Guid CompanyGUID)
        {
            var _exisitngCompany = await GetCompanyByGUIDAsync(CompanyGUID);

            if (_exisitngCompany != null)
            {
                _exisitngCompany.IsDeleted = true;
                return await Save();
            }
            return false;
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }

    }
}
