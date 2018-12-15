using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using CityAppREST.Data.Repositories;
using CityAppREST.Filters;
using CityAppREST.Helpers;
using CityAppREST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace CityAppREST.Controllers {
	/// <summary>
	/// Contains CRUD endpoints to manage users
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : Controller {
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Company> _companyRepository;
		private readonly TokenGenerator _tokenGenerator;

		public UsersController(IRepository<User> userRepository, IRepository<Company> companyRepository, TokenGenerator tokenGenerator)
		{
			_userRepository = userRepository;
			_companyRepository = companyRepository;
			_tokenGenerator = tokenGenerator;
		}

		/// <summary>
		/// Calls the repository to return all users.
		/// </summary>
		/// <returns>List of Users</returns>
		/// <response code="200">Returns List of users</response>
		/// <response code="401">Unauthorized: : Request must contain a valid bearer token and contain a Claim of type Role and value Admin</response>
		// GET: api/users

		[Authorize(Policy = nameof(UserType.Admin))]
		[HttpGet]
		public IEnumerable<User> Get()
		{
			return _userRepository.GetAll();
		}

		/// <summary>
		/// Get the user with specified id.
		/// </summary>
		/// <returns>A user or NotFound if no user is found with specified id</returns>
		/// <param name="id">User id</param>
		/// <response code="200">Returns a User</response>
		/// <response code="401">Unauthorized: must be authenticated</response>
		/// <response code="403">Forbidden: Only read access to own user</response>
		/// <response code="404">Not Found: no user with supplied id</response>
		// GET api/users/5
		[ReadWriteAccessFilter(RequestObjectType = nameof(User))]
		[HttpGet("{id}")]
		public ActionResult<User> Get(int id)
		{
			var user = _userRepository.GetById(id);
			return (ActionResult<User>)user ?? NotFound();
		}

		/// <summary>
		/// Get the user with specified username.
		/// </summary>
		/// <returns>A user or NotFound if no user is found with specified id</returns>
		/// <param name="username">User username</param>
		/// <response code="200">Returns a User</response>
		/// <response code="401">Unauthorized: must be authenticated</response>
		/// <response code="403">Forbidden: Only read access to own user</response>
		/// <response code="404">Not Found: no user with supplied id</response>
		// GET api/users/5
		[ReadWriteAccessFilter(RequestObjectType = nameof(User))]
		[HttpGet("{username}")]
		public ActionResult<User> Get(string username)
		{
			var user = (_userRepository as UserRepository).GetByUsername(username);
			return (ActionResult<User>)user ?? NotFound();
		}

		/// <summary>
		/// Post the specified user. Hashes the password with salt and calls the repository to create a new user.
		/// </summary>
		/// <returns>The created user</returns>
		/// <param name="user">User object to create</param>
		/// <response code="200">The created user</response>
		// POST api/users
		[AllowAnonymous]
		[HttpPost]
		public ActionResult<User> Post(User user)
		{
			user.Password = PasswordHasher.GetPasswordAndSaltHash(user.Password);
			_userRepository.Create(user);
			_userRepository.SaveChanges();
			return Ok();
		}


		/// <summary>
		/// Put the specified id and user.
		/// </summary>
		/// <returns>The edited user</returns>
		/// <param name="id">User id</param>
		/// <param name="user">Userobject</param>
		/// <response code="200">The edited user</response>
		/// <response code="401">Unauthorized: Request must contain a valid bearer token</response>
		/// <response code="403">Forbidden: Only write access to own user</response>
		/// <response code="404">Not Found: no user with supplied id</response>
		// PUT api/users/5
		[ReadWriteAccessFilter(RequestObjectType = nameof(User))]
		[HttpPut("{id}")]
		public ActionResult<User> Put(int id, User user)
		{
			var toUpdate = _userRepository.GetById(id);
			if (toUpdate == null)
			{
				return NotFound();
			}
			user.Id = toUpdate.Id;
			_userRepository.Update(user);
			_userRepository.SaveChanges();

			return toUpdate;
		}

		/// <summary>
		/// Delete the user with specified id.
		/// </summary>
		/// <returns>The deleted user or a NotFound when no user with specified id is found</returns>
		/// <param name="id">User id</param>
		/// <response code="200">The deleted user</response>
		/// <response code="401">Unauthorized: Request must contain a valid bearer token</response>
		/// <response code="403">Forbidden: Only write access to own user</response>
		/// <response code="404">Not Found: no user with supplied id</response>
		// DELETE api/users/5
		[ReadWriteAccessFilter(RequestObjectType = nameof(User))]
		[HttpDelete("{id}")]
		public ActionResult<User> Delete(int id)
		{
			var toDelete = _userRepository.GetById(id);
			if (toDelete == null)
			{
				return NotFound();
			}

			_userRepository.Delete(id);
			_userRepository.SaveChanges();

			return toDelete;
		}

		/// <summary>
		/// Authenticate the specified loginDetails.
		/// </summary>
		/// <returns>A jwt token for further authentication</returns>
		/// <param name="loginDetails">Details containing username and password to authenticate with</param>
		/// <response code="200">Returns a token for further authentication</response>
		/// <response code="401">Unauthorized: Password did not match</response>
		/// <response code="404">Not Found: no user with specified username in logindetails</response>
		// POST api/users/authenticate
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public ActionResult Authenticate(LoginDetails loginDetails)
		{
			var user = (_userRepository as UserRepository).GetByUsername(loginDetails.Username);
			if (user == null)
			{
				return NotFound();
			}

			var passwordsMatch = PasswordHasher.VerifyPasswordWithHash(loginDetails.Password, user.Password);
			if (!passwordsMatch)
			{
				return Unauthorized();
			}

			var token = _tokenGenerator.GenerateTokenForUser(user);
			return Ok(new { userId = user.Id, username = user.Username, token });
		}

		/// <summary>
		/// Add company to the subscription list of the user
		/// </summary>
		/// <returns>The updated subscription list</returns>
		/// <param name="id">user id</param>
		/// <param name="company">companyId</param>
		/// <returns>List of the user subscription</returns>
		/// <response code="200">Returns a list of the subscriptions of the user</response>
		// POST api/users/{id}/companies
		[AllowAnonymous]
		[HttpPost("{id}/companies")]
		public IEnumerable<Company> Companies(int id, Company company)
		{
			var user = (_userRepository as UserRepository).GetById(id);

			// Update company with latest details
			var companyResult = _companyRepository.GetById(company.Id);

			user.Subscriptions.Add(companyResult);
			_userRepository.SaveChanges();

			return user.Subscriptions;
		}
	}

	public class LoginDetails {
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
