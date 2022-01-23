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
    public class AuthorController : Controller
    {

        private IApplicationDbContext _context;
        private IAuthorRepository _authorRepository;
        public AuthorController(IApplicationDbContext context, IAuthorRepository authorRepository)
        {
            _context = context;
            _authorRepository = authorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            _authorRepository.Add(author);
            await _context.SaveChanges();
            return Ok(author.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = _authorRepository.GetAll();
            if (authors == null) return NotFound();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null) return NotFound();
            _authorRepository.Remove(author);
            await _context.SaveChanges();
            return Ok(author.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Author newauthor)
        {
            var author = _authorRepository.GetById(id);
            if (author == null) return NotFound();
            else
            {
                author.FirstName = newauthor.FirstName;
                author.LastName = newauthor.LastName;
                await _context.SaveChanges();
                return Ok(author.Id);
            }
        }

        
       

    }
}
