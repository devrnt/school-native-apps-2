using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityAppREST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityAppREST.Controllers
{
    /// <summary>
    /// Contains CRUD endpoints to manage companies
    /// </summary>
    [Authorize(Policy = "Owner, Admin")]
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
        // GET: api/companies
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Company>> Get()
        {
            return _companyRepository.GetAll().ToList();
        }

        /// <summary>
        /// Calls the repository to find a company with a specified Id
        /// </summary>
        /// <returns>A company or a NotFoundResult when no company matching the id is found</returns>
        /// <param name="id">Company id</param>
        // GET api/companies/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Company> Get(int id)
        {
            var company = _companyRepository.GetById(id);
            return company == null ? (ActionResult<Company>)NotFound() : (ActionResult<Company>)company;
        }

        /// <summary>
        /// Post the specified company. Calls the repository to create a new company.
        /// </summary>
        /// <returns>Posted company</returns>
        /// <param name="company">Company to create</param>
        // POST api/companies
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
        // PUT api/companies/5
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
        // DELETE api/companies/5
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
