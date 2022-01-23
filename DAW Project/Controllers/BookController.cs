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
    public class BookController : Controller
    {

        private IApplicationDbContext _context;
        private IBookRepository _bookRepository;
        public BookController(IApplicationDbContext context, IBookRepository BookRepository)
        {
            _context = context;
            _bookRepository = BookRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            _bookRepository.Add(book);
            await _context.SaveChanges();
            return Ok(book.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = _bookRepository.GetAll();
            if (books == null) return NotFound();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            _bookRepository.Remove(book);
            await _context.SaveChanges();
            return Ok(book.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book newbook)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            else
            {
                book.Genre = newbook.Genre;
                book.ReleaseDate = newbook.ReleaseDate;
                
                await _context.SaveChanges();
                return Ok(book.Id);
            }
        }

        
       

    }
}
