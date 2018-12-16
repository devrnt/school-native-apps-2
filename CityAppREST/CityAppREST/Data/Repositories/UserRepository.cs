using System;
using System.Collections.Generic;
using System.Linq;
using CityAppREST.Helpers;
using CityAppREST.Models;
using Microsoft.EntityFrameworkCore;

namespace CityAppREST.Data.Repositories {
	public class UserRepository : IRepository<User> {
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly DbSet<User> _users;

		public UserRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			_users = _applicationDbContext.Users;
		}

		public void Create(User toCreate)
		{
			_users.Add(toCreate);
		}

		public void Delete(int id)
		{
			var user = _users.FirstOrDefault(u => u.Id == id);
			if (user != null)
			{
				_users.Remove(user);
			}
		}

		public IEnumerable<User> GetAll()
		{
			return _users.ToList();
		}

		public User GetById(int id)
		{
			return _users.Include(u => u.Companies).Include(u => u.Subscriptions).FirstOrDefault(u => u.Id == id);
		}

		public void SaveChanges()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(User toUpdate)
		{
			var user = _users.FirstOrDefault(u => u.Id == toUpdate.Id);

			if (user != null)
			{
				user.Companies = toUpdate.Companies;
			}
		}

		public User GetByUsername(string username)
		{
			return _users.Include(u => u.Companies).FirstOrDefault(u => u.Username == username);
		}
	}
}
