using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Roll
        public IEnumerable<Role> GetRoles()
        {
            return _context.Role.ToList();
        }

        //Fetch Role by ID
        public Role? GetById(int id)
        {
            return _context.Role.Find(id);
        }

        // Add new Role
        public void Add(Role role)
        {
            _context.Role.Add(role);
            _context.SaveChanges();
        }


        // Update Role
        public void Update(Role updatedRole)
        {
            var role = _context.Role.Find(updatedRole.Id);
            if (role != null)
            {
                role.RoleName = updatedRole.RoleName;
                role.IsActive = updatedRole.IsActive;

                _context.SaveChanges();
            }
        }
    }
}
