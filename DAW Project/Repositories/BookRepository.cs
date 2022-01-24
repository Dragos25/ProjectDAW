using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Context;
using DAW_Project.Interfaces;
using DAW_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace DAW_Project.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {

        }

        public new IEnumerable<Book> GetAll()
        {
            var query = from book in _context.Books.Include(a => a.Author).Include(p => p.Publisher) select book;
            return query;

        }
    }
}
