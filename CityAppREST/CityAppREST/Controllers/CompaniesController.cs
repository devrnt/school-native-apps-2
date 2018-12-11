using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityAppREST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityAppREST.Controllers
{
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

        // GET: api/companies
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Company>> Get()
        {
            return _companyRepository.GetAll().ToList();
        }

        // GET api/companies/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Company> Get(int id)
        {
            var company = _companyRepository.GetById(id);
            return company == null ? (ActionResult<Company>)NotFound() : (ActionResult<Company>)company;
        }

        // POST api/companies
        [HttpPost]
        public ActionResult<Company> Post(Company company)
        {
            _companyRepository.Create(company);
            _companyRepository.SaveChanges();
            return company;
        }

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
