using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.Dtos.Company;
using WebApiSeriesCore5.Service.Contract;

namespace WebApiSeriesCore5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanyDto>))]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetCompaniesAsync();

            return Ok(companies);
        }

        [HttpGet("{CompanyID:int}", Name = "GetByCompanyID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CompanyDto>> GetCompanyID(int CompanyID)
        {
            if (CompanyID <=0)
            {
                return BadRequest(CompanyID);
            }
            var CompanyFound = await _companyService.GetByIdAsync(CompanyID);

            if (CompanyFound.Data == null)
            {
                return NotFound();
            }

            return Ok(CompanyFound);
           
        }

       

        /// <summary>
        /// Get Company by GUID.
        /// </summary>
        /// <param name="CompanyGUID"></param>
        /// <returns></returns>
        //GET/companies/123
        [HttpGet("{CompanyGUID:Guid}", Name = "GetCompanyByGUID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CompanyDto>> GetByGUID(Guid CompanyGUID)
        {

            if (CompanyGUID == Guid.Empty)
            {
                return BadRequest(CompanyGUID);
            }

            var company = await _companyService.GetByGUIDAsync(CompanyGUID);

            if (company.Data == null)
            {

                return NotFound();
            }

            return Ok(company);
        }

        /// <summary>
        /// Create a new company Record.
        /// </summary>
        /// <param name="createCompanyDto"></param>
        /// <returns></returns>
        //POST /Companies
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CreateCompanyDto createCompanyDto)
        {
            if (createCompanyDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newCompany = await _companyService.AddCompanyAsync(createCompanyDto);

            if (_newCompany.Success == false && _newCompany.Message == "Exist")
            {
                return Ok(_newCompany);
            }


            if (_newCompany.Success == false && _newCompany.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding company {createCompanyDto}");
                return StatusCode(500, ModelState);
            }

            if (_newCompany.Success == false && _newCompany.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when adding company {createCompanyDto}");
                return StatusCode(500, ModelState);
            }

            //Return new company created
            return CreatedAtRoute("GetCompanyByGUID", new { CompanyGUID = _newCompany.Data.GUID }, _newCompany);

        }

        /// <summary>
        /// Update existing company record.
        /// </summary>
        /// <param name="CompanyGUID"></param>
        /// <param name="updateCompanyDto"></param>
        /// <returns></returns>
        //PUT/Companies/id
        [HttpPut("{CompanyGUID:Guid}", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCompany(Guid CompanyGUID, [FromBody] UpdateCompanyDto updateCompanyDto)
        {
            if (updateCompanyDto == null || updateCompanyDto.GUID != CompanyGUID)
            {
                return BadRequest(ModelState);
            }


            var _updateCompany = await _companyService.UpdateCompanyAsync(updateCompanyDto);

            if (_updateCompany.Success == false && _updateCompany.Message == "NotFound")
            {
                return Ok(_updateCompany);
            }

            if (_updateCompany.Success == false && _updateCompany.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating company {updateCompanyDto}");
                return StatusCode(500, ModelState);
            }

            if (_updateCompany.Success == false && _updateCompany.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when updating company {updateCompanyDto}");
                return StatusCode(500, ModelState);
            }


            return Ok(_updateCompany);
        }

        /// <summary>
        /// Mark a record as deleted .
        /// </summary>
        /// <param name="CompanyGUID"></param>
        /// <returns></returns>
        //DELETE /companies/{id}
        [HttpDelete("{CompanyGUID:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompany(Guid CompanyGUID)
        {

            var _deleteCompany = await _companyService.SoftDeleteCompanyAsync(CompanyGUID);


            if (_deleteCompany.Success == false && _deleteCompany.Data == "NotFound")
            {
                ModelState.AddModelError("", "Company Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteCompany.Success == false && _deleteCompany.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting company");
                return StatusCode(500, ModelState);
            }

            if (_deleteCompany.Success == false && _deleteCompany.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting company");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
