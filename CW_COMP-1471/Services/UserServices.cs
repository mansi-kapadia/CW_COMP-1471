using CW_COMP_1471.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all users
        public IEnumerable<User> GetUsers()
        {
            if(_context.Users.Any())
            {
                return _context.Users.ToList();
            }
            return Enumerable.Empty<User>();
        }

        // Get user by ID
        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Add a new user
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Update an existing user
        public void UpdateUser(User updatedUser)
        {
            var user = _context.Users.Find(updatedUser.Id);
            if (user != null)
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.UserName = updatedUser.UserName;
                user.Password = updatedUser.Password;
                user.RoleId = updatedUser.RoleId;
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;
                user.Address = updatedUser.Address;

                _context.SaveChanges();
            }
        }

        // Delete a user
        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
