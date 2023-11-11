using Microsoft.EntityFrameworkCore;
using StudioData.Interfaces;
using StudioData.Models.Business;
using StudioData.Models.Loggin;
using StudioData.Data;

namespace StudioData.Services
{
    public class UserService : IUserService
    {
        private readonly StudioWebContext _context;
        private readonly ICommonServices<User> _commonContext;

        public UserService(StudioWebContext context, ICommonServices<User> commonContext)
        {
            _context = context;
            _commonContext = commonContext;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _commonContext.GetAllAsync();
        }
        public async Task<User> GetByIdAsync(Guid Id)
        {
            return await _commonContext.GetByIdAsync(Id);
        }        
        public User GetById(Guid Id)
        {
            return _context.Users.Where(u => u.Id == Id).FirstOrDefault();
        }
        public async Task<bool> UpdateAsync(User user)
        {
            return await _commonContext.UpdateAsync(user);
        }
        public async Task<bool> InsertAsync(User user)
        {
            return await _commonContext.InsertAsync(user);

        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            return await _commonContext.DeleteAsync(Id);
        }
        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }
        public async Task<User> ValidateUserAsync(UserLoggin userLoggin)
        {
            return await _context.Users.FirstOrDefaultAsync(us => us.Email == userLoggin.UserMail && us.Password == userLoggin.Password);
        }

    }
}
