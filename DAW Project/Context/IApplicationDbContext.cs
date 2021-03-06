using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace DAW_Project.Context
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Publisher> Publishers { get; set; }
        DbSet<Book> Books { get; set; }
        



        Task<int> SaveChanges();
    }
}
