using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles(bool IsActive);
        Role? GetById(int id);

        void Add(Role role);

        void Update(Role updatedRole);
    }
}
