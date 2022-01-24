using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Authentication;
using DAW_Project.Constants;
using DAW_Project.Context;
using DAW_Project.Interfaces;
using DAW_Project.Models;
using DAW_Project.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAW_Project.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class UserController : Controller
    {

        private IConfiguration _config;
        private IApplicationDbContext _context;
        private IUserRepository _userRepository;
        public UserController(IApplicationDbContext context, IUserRepository userRepository, IConfiguration config)
        {
            _context = context;
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            User existing = _userRepository.GetExactMatch(user.username);
            if (existing != null) return BadRequest("Username-ul deja exista");
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.password);
            user.password = passwordHash;
            user.isAdmin = false;
            _userRepository.Add(user);
            await _context.SaveChanges();
            return Ok(user.Id);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            User existing = _userRepository.GetExactMatch(user.username);
            if (existing == null) return BadRequest("Username-ul nu exista");
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.password);

            if (BCrypt.Net.BCrypt.Verify(user.password, existing.password))
            {
                String tokenstring = Util.GenerateToken(existing, _config);
                Tokenclass token = new Tokenclass();
                token.token = tokenstring;
                return Ok(token); 
            }
            return BadRequest("Parola gresita");

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var currentUser = HttpContext.User;
            if(currentUser.HasClaim(c=> c.Type == "isAdmin"))
            {
                var users = _userRepository.GetAll();
                if (users == null) return NotFound();
                return Ok(users);
            }
            
            return Unauthorized();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            _userRepository.Remove(user);
            await _context.SaveChanges();
            return Ok(user.Id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, User newUser)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            else
            {
                user.firstName = newUser.firstName;
                user.lastName = newUser.lastName;
                await _context.SaveChanges();
                return Ok(user.Id);
            }
        }

        
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(String username)
        {
            var users = _userRepository.GetUserByUsername(username);
            if (users == null) return NotFound();
            return Ok(users);
        }


        

    }
}
