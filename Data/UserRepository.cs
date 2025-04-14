using Data.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        private readonly WorklogContext _context;

        public UserRepository(WorklogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByName(string nameUser)
        {
            return (await  _context.Users.Where(c => c.Name.Equals(nameUser)).FirstOrDefaultAsync());
        }
    }
}
