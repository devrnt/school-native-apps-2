using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityAppREST.Filters;
using CityAppREST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityAppREST.Controllers
{
    /// <summary>
    /// Contains CRUD endpoints to manage companies
    /// </summary>
    [Authorize(Policy = "Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepository<Company> _companyRepository;

        public CompaniesController(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Calls the repository to return a list of all companies
        /// </summary>
        /// <returns>List of all companies</returns>
        /// <response code="200">Returns a list of companies</response>
        // GET: api/companies
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Company> Get()
        {
            return _companyRepository.GetAll();
        }

        /// <summary>
        /// Calls the repository to find a company with a specified Id
        /// </summary>
        /// <returns>A company or a NotFoundResult when no company matching the id is found</returns>
        /// <param name="id">Company id</param>
        /// <response code="200">Returns a company whose id matches the supplied id</response>
        /// <response code="404">Not Found: no such company whose id matches the supplied id</response>
        // GET api/companies/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Company> Get(int id)
        {
            var company = _companyRepository.GetById(id);
            return (ActionResult<Company>)company ?? NotFound();
        }

        /// <summary>
        /// Post the specified company. Calls the repository to create a new company.
        /// </summary>
        /// <returns>Posted company</returns>
        /// <param name="company">Company to create</param>
        /// <response code="200">Returns the created company</response>
        /// <response code="401">Unauthorized: request must contain a valid bearer token and contain a Claim of type Role and value Owner</response>
        // POST api/companies
        [ReadWriteAccessFilter(RequestObjectType = nameof(Company))]
        [HttpPost]
        public ActionResult<Company> Post(Company company)
        {
            _companyRepository.Create(company);
            _companyRepository.SaveChanges();
            return company;
        }

        /// <summary>
        /// Put the specified id and company.
        /// </summary>
        /// <returns>The edited company</returns>
        /// <param name="id">Company id</param>
        /// <param name="company">Company object</param>
        /// <response code="200">Returns the edited company</response>
        /// <response code="401">Unauthorized: request must contain a valid bearer token and contain a Claim of type Role and value Owner</response>
        /// <response code="403">Forbidden: Only the owner of specified company has write access</response>
        // PUT api/companies/5
        [ReadWriteAccessFilter(RequestObjectType = nameof(Company))]
        [HttpPut("{id}")]
        public ActionResult<Company> Put(int id, Company company)
        {
            var toUpdate = _companyRepository.GetById(id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            company.Id = toUpdate.Id;
            _companyRepository.Update(company);
            _companyRepository.SaveChanges();

            return toUpdate;
        }

        /// <summary>
        /// Delete the company with specified id.
        /// </summary>
        /// <returns>The deleted company or a NotFound if no company is found with specified id</returns>
        /// <param name="id">Company id</param>
        /// <response code="200">Returns the deleted company</response>
        /// <response code="401">Unauthorized: request must contain a valid bearer token and contain a Claim of type Role and value Owner</response>
        /// <response code="403">Forbidden: Only the owner of specified company has write access</response>
        // DELETE api/companies/5
        [ReadWriteAccessFilter(RequestObjectType = nameof(Company))]
        [HttpDelete("{id}")]
        public ActionResult<Company> Delete(int id)
        {
            var toDelete = _companyRepository.GetById(id);
            if (toDelete == null)
            {
                return NotFound();
            }

            _companyRepository.Delete(id);
            _companyRepository.SaveChanges();

            return toDelete;
        }
    }
}
