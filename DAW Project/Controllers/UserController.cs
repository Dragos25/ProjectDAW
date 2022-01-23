using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Context;
using DAW_Project.Interfaces;
using DAW_Project.Models;
using DAW_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAW_Project.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class UserController : Controller
    {

        private IApplicationDbContext _context;
        private IUserRepository _userRepository;
        public UserController(IApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            _userRepository.Add(user);
            await _context.SaveChanges();
            return Ok(user.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userRepository.GetAll();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();
            _userRepository.Remove(user);
            await _context.SaveChanges();
            return Ok(user.Id);
        }

        [HttpPut("{id}")]
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
