using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class UserServices
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Password = "pass123", RoleId = 1, Email = "john@example.com", PhoneNumber = "9876543210", Address = "123 Main St, New York" },
            new User { Id = 2, FirstName = "Jane", LastName = "Smith", UserName = "janesmith", Password = "pass456", RoleId = 2, Email = "jane@example.com", PhoneNumber = "8765432109", Address = "456 Oak St, California" },
            new User { Id = 3, FirstName = "Mike", LastName = "Johnson", UserName = "mikejohn", Password = "pass789", RoleId = 1, Email = "mike@example.com", PhoneNumber = "7654321098", Address = "789 Pine St, Texas" },
            new User { Id = 4, FirstName = "Emily", LastName = "Davis", UserName = "emilyd", Password = "pass101", RoleId = 2, Email = "emily@example.com", PhoneNumber = "6543210987", Address = "321 Cedar St, Florida" },
            new User { Id = 5, FirstName = "David", LastName = "Wilson", UserName = "davidw", Password = "pass202", RoleId = 3, Email = "david@example.com", PhoneNumber = "5432109876", Address = "555 Maple St, Chicago" }
        };
    

        // Get all users
        public static List<User> GetUsers()
        {
            return users;
        }

        // Get user by ID
        public static User GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        // Add a new user
        public static void AddUser(User user)
        {
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
        }

        // Update an existing user
        public static void UpdateUser(User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
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
            }
        }

        // Delete a user
        public static void DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
            }
        }
    }
}
