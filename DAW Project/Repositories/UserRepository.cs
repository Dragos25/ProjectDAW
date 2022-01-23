using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Context;
using DAW_Project.Interfaces;
using DAW_Project.Models;

namespace DAW_Project.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IEnumerable<User> GetUserByUsername(String text)
        {
            return _context.Users.Where(u => u.username.Contains(text)).ToList();
        }
    }
}
