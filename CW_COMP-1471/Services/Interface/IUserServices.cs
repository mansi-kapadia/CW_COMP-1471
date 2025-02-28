using CW_COMP_1471.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        User? GetUserById(int id);

        void AddUser(User user);

        void UpdateUser(User updatedUser);

        void DeleteUser(int id);
    }
}
