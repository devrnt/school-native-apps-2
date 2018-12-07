using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityAppREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityAppREST.Controllers
{
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly IRepository<Company> _companyRepository;

        public CompaniesController(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        // GET: api/companies
        [HttpGet]
        public ActionResult<List<Company>> Get()
        {
            return _companyRepository.GetAll().ToList();
        }

        // GET api/companies/5
        [HttpGet("{id}")]
        public ActionResult<Company> Get(int id)
        {
            var company = _companyRepository.GetById(id);
            return company == null ? (ActionResult<Company>)NotFound() : (ActionResult<Company>)company;
        }

        // POST api/companies
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/companies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/companies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
