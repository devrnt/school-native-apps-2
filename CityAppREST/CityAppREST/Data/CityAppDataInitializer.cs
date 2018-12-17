using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CityAppREST.Helpers;
using CityAppREST.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CityAppREST.Data {
	public class CityAppDataInitializer {
		private readonly ApplicationDbContext _applicationDbContext;


		public CityAppDataInitializer(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public void InitializeData()
		{
			_applicationDbContext.Database.EnsureDeleted();

			if (_applicationDbContext.Database.EnsureCreated())
			{
				#region Companies
				var companies = new List<Company>
				{
					new Company(
						"Swashy",
						"Hier kunt u terrecht met al uw vuile was. Op 15min. kunt u terug keren met een vers gewassen was. Was u er vorige keer niet bij, kom dan nu zeker langs met uw was.",
						"was laundry Swashy",
						Categories.Wasserij,
						new List<Location> { new Location("Belgium", "Gent", 9000, "Grensstraat", 245) },
						new List<OpeningHours> {
							new OpeningHours(Days.Maandag, new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 17, 0, 0)),
							new OpeningHours(Days.Dinsdag, new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 17, 0, 0)),
							new OpeningHours(Days.Woensdag, new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 17, 0, 0)),
							new OpeningHours(Days.Donderdag, new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 17, 0, 0)),
							new OpeningHours(Days.Vrijdag, new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 15, 0, 0))
						},
						new LeaveOfAbsence(),
						new SocialMedia("https://www.jonasdevrient.be","https://www.jonasdevrient.be","https://www.jonasdevrient.be","https://www.jonasdevrient.be"),
						new List<Promotion> {
							new Promotion("Korting op uw eerste was"),
							new Promotion("2 wassen = 3 betalen")
						})
                    { Discounts = new List<Discount>(){ new Discount("couponcode", "") },
                        Events = new List<Event>(){ new Event("Opendeurdag", "U bent welkom om in onze state-of-the art wasserij.", new DateTime(2000, 1, 1, 17, 0, 0), "http://www.cohousinglimburg.be/wp-content/uploads/2017/11/opendeur-2.gif") } },
					new Company{ Name = "Bank van Gent", Description = "Voor al uw geldzaken", Categorie = Categories.Bank },
					new Company{ Name = "Bakkerij Koen", Description = "Uw warme bakker", Categorie = Categories.Bakkerij }
				};

				_applicationDbContext.Companies.AddRange(companies);
				_applicationDbContext.SaveChanges();
				#endregion

				#region Users
				var users = new List<User>
				{
					new User("Dhondt", "Sam", "dhondtsam", new DateTime(1993, 7, 5), "sam.dhondt@hogent.be", PasswordHasher.GetPasswordAndSaltHash("samsamsam"), new List<Company>{ companies[0]}, UserType.Visitor),
					new User("Ouahab", "Yanis", "ouahabyanis", new DateTime(2000, 1, 1), "yanis.ouahab@hogent.be", PasswordHasher.GetPasswordAndSaltHash("yanisyanis"), new List<Company> { companies[0] }, UserType.Owner),
					new User("De Vrient", "Jonas", "devrientjonas", new DateTime(2000, 1, 1), "jonas.devrient@hogent.be", PasswordHasher.GetPasswordAndSaltHash("jonasjonas"), new List<Company>(), UserType.Admin)
				};

				_applicationDbContext.Users.AddRange(users);
				_applicationDbContext.SaveChanges();
				#endregion

			}
		}
	}
}
