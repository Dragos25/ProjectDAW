using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Models;

namespace DAW_Project.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetUserByUsername(String text);
    }
}
