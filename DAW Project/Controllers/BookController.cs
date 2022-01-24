using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Context;
using DAW_Project.Interfaces;
using DAW_Project.Models;
using DAW_Project.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private IPublisherRepository _publisherRepository;
        private IAuthorRepository _authorRepository;
        public BookController(IApplicationDbContext context, IBookRepository BookRepository, IPublisherRepository publisherRepository, IAuthorRepository authorRepository)
        {
            _context = context;
            _bookRepository = BookRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book, int? authorId, int? publisherId)
        {
            if (authorId == null || publisherId == null)
                return BadRequest("missing authorId or publisherId");
            Author author = _authorRepository.GetById(authorId.Value);
            if (author == null)
                return NotFound("Author not found");
            Publisher publisher = _publisherRepository.GetById(publisherId.Value);
            if (publisher == null)
                return NotFound("Publisher not found");
            book.Author = author;
            book.Publisher = publisher;
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            _bookRepository.Remove(book);
            await _context.SaveChanges();
            return Ok(book.Id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, Book newbook)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) return NotFound();
            else
            {
                book.Name = newbook.Name;
                book.Genre = newbook.Genre;
                book.ReleaseDate = newbook.ReleaseDate;
                
                await _context.SaveChanges();
                return Ok(book.Id);
            }
        }

        
       

    }
}
