using WebApplication1.DatabaseProvider;

namespace WebApplication1.Services
{
    public static class RoleService
    {
        private static readonly InventoryRfidContext _context;

         static RoleService()
        {
            _context = new InventoryRfidContext(); ;
        }


        public static string? GetRolesOf(string name)
        {
            return _context.Users.Where(A => A.Username.Equals(name)).Select(a => a.Role.Name).FirstOrDefault();
        }

        internal static void AddNewUserRole(string name)
        {
            if(!_context.Users.Any(a => a.Username.Equals(name)))
            {
                _context.Users.Add(new User()
                {
                    Username = name,
                    RoleId = 2,
                });
                _context.SaveChanges();
            }
        }
    }
}
