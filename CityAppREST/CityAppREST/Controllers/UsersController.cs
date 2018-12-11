using System.Collections.Generic;
using System.Linq;
using CityAppREST.Data.Repositories;
using CityAppREST.Helpers;
using CityAppREST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityAppREST.Controllers
{
    /// <summary>
    /// Contains CRUD endpoints to manage users
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly TokenGenerator _tokenGenerator;

        public UsersController(IRepository<User> userRepository, TokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Calls the repository to return all users.
        /// </summary>
        /// <returns>List of Users</returns>
        // GET: api/users
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
        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userRepository.GetById(id);
            return (ActionResult<User>)user ?? NotFound();
        }

        /// <summary>
        /// Post the specified user. Hashes the password with salt and calls the repository to create a new user.
        /// </summary>
        /// <returns>The created user</returns>
        /// <param name="user">User object</param>
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
        // PUT api/users/5
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
        // DELETE api/users/5
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
        /// <param name="loginDetails">Details containg username and password to check</param>
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
            return Ok(new { token });
        }
    }

    public class LoginDetails
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
