using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByLoginAsync(string Login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == Login);
        }

        public async Task<User> CreateAsync(User userModel)
        {
            var user = await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
    }
}
