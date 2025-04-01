using CW_COMP_1471.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                return _context.Users.Include(x => x.Role).ToList();
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
                if(!string.IsNullOrEmpty(updatedUser.Password)) // if user leaves password empty keep the old password
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

                var reviews = _context.Reviews.Where(x => x.ReviewerId == id).ToList();

                if (reviews.Any())
                {
                    _context.Reviews.RemoveRange(reviews);
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // Get User model with Customer role 
        public User GetRegisterModel()
        {
            return new User
            {
                RoleId = _context.Role.FirstOrDefault(r => r.RoleName == "Customer")?.Id ?? 2,
            };
        }

        public bool RegisterUser(User model)
        {
            if (_context.Users.Any(u => u.UserName == model.UserName))
                return false;

            _context.Users.Add(model);
            _context.SaveChanges();
            return true;
        }

        public bool CheckPassword(LoginViewModel loginViewModel)
        {
            User user = _context.Users.First(x => x.UserName == loginViewModel.Username);
            if (user == null)
                return false;
                
            return user.Password == loginViewModel.Password;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }
    }
}
