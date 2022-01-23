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
    public class PublisherController : Controller
    {

        private IApplicationDbContext _context;
        private IPublisherRepository _PublisherRepository;
        public PublisherController(IApplicationDbContext context, IPublisherRepository PublisherRepository)
        {
            _context = context;
            _PublisherRepository = PublisherRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Publisher Publisher)
        {
            _PublisherRepository.Add(Publisher);
            await _context.SaveChanges();
            return Ok(Publisher.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Publishers = _PublisherRepository.GetAll();
            if (Publishers == null) return NotFound();
            return Ok(Publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Publisher = _PublisherRepository.GetById(id);
            if (Publisher == null) return NotFound();
            return Ok(Publisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Publisher = _PublisherRepository.GetById(id);
            if (Publisher == null) return NotFound();
            _PublisherRepository.Remove(Publisher);
            await _context.SaveChanges();
            return Ok(Publisher.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Publisher newPublisher)
        {
            var Publisher = _PublisherRepository.GetById(id);
            if (Publisher == null) return NotFound();
            else
            {
                Publisher.Name = newPublisher.Name;
                Publisher.Location = newPublisher.Location;
                Publisher.Website = newPublisher.Website;
                await _context.SaveChanges();
                return Ok(Publisher.Id);
            }
        }

        
       

    }
}
