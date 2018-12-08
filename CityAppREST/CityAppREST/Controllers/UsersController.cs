using System.Collections.Generic;
using System.Linq;
using CityAppREST.Helpers;
using CityAppREST.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityAppREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.GetAll();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userRepository.GetById(id);
            return user == null ? (ActionResult<User>)NotFound() : (ActionResult<User>)user;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            user.Password = PasswordHasher.GetPasswordAndSaltHash(user.Password);
            _userRepository.Create(user);
            _userRepository.SaveChanges();
            return Ok();
        }

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

        [HttpPost("authenticate")]
        public ActionResult<User> Authenticate(LoginDetails loginDetails)
        {
            return Ok();
        }
    }

    public class LoginDetails
    {
        string Username { get; set; }
        public string Password { get; set; }
    }
}
