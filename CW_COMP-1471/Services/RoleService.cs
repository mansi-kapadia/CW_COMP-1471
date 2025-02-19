using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class RoleService
    {
        private static List<Role> roles =  new List<Role>
            {
                new Role { Id = 1, RoleName = "Admin"  },
                new Role { Id = 2, RoleName = "User" },
                new Role { Id = 3, RoleName = "Manager" }
            };

        public static List<Role> GetRoles()
        {
            return roles;  
        }

        public static Role GetById(int id)
        {
            return roles.FirstOrDefault(u => u.Id == id);
        }

        // Add a new user
        public static void Add(Role role)
        {
            role.Id = roles.Any() ? roles.Max(u => u.Id) + 1 : 1;
            roles.Add(role);
        }

        // Update an existing role
        public static void Update(Role updatedRole)
        {
            var role = roles.FirstOrDefault(u => u.Id == updatedRole.Id);
            if (role != null)
            {
                role.RoleName = updatedRole.RoleName;
                role.IsActive = updatedRole.IsActive;
            }
        }
    }
}
