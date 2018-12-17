using System.Collections.Generic;
using System.Linq;
using CityAppREST.Models;
using Microsoft.EntityFrameworkCore;

namespace CityAppREST.Data.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Company> _companies;

        public CompanyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _companies = applicationDbContext.Companies;
        }

        public void Create(Company toCreate)
        {
            _companies.Add(toCreate);
        }

        public void Delete(int id)
        {
            var toDelete = _companies.FirstOrDefault(c => c.Id == id);

            if (toDelete != null)
                _companies.Remove(toDelete);
        }

        public IEnumerable<Company> GetAll()
        {
            return _companies.Include(c => c.Promotions).ToList();
        }

        public Company GetById(int id)
        {
            return _companies
                             .Include(c => c.Discounts)
                             .Include(c => c.LeaveOfAbsence)
                             .Include(c => c.Locations)
                             .Include(c => c.OpeningHours)
                             .Include(c => c.Promotions)
                                .ThenInclude(p => p.Discount)
                             .Include(c => c.SocialMedia)
                             .Include(c => c.Events)
                             .FirstOrDefault(c => c.Id == id);
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(Company toUpdate)
        {
            var company = _companies.FirstOrDefault(c => c.Id == toUpdate.Id);

            if (company != null)
            {
                company.Description = toUpdate.Description;
                company.Discounts = toUpdate.Discounts;
                company.LeaveOfAbsence = toUpdate.LeaveOfAbsence;
                company.Locations = toUpdate.Locations;
                company.OpeningHours = toUpdate.OpeningHours;
                company.Promotions = toUpdate.Promotions;
                company.SocialMedia = toUpdate.SocialMedia;
            }
        }
    }
}
