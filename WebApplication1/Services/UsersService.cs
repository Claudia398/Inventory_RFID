using Microsoft.EntityFrameworkCore;
using WebApplication1.DatabaseProvider;
using WebApplication1.DTO;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly InventoryRfidContext _context;

        public UserService(InventoryRfidContext context)
        {
            _context = context;
        }

        public List<UserDTO> GetAll()
        {
            return _context.Users
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    RoleId = u.RoleId,
                    Username = u.Username
                })

                .ToList();
        }

        public UserDTO? GetById(int id)
        {
            return _context.Users
                .Where(x => x.Id == id)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    RoleId = u.RoleId,
                    Username = u.Username
                })
                .FirstOrDefault();
        }
       

        public void Add(UserDTO userDto)
        {
            var user = new User
            {
                Username = userDto.Username
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(UserDTO userDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userDto.Id);

            if (user != null)
            {
                user.Username = userDto.Username;
                user.RoleId = userDto.RoleId;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var item = _context.Users.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Users.Remove(item);
                _context.SaveChanges();
            }
        }

        internal UserDTO? GetByUserName(string? username)
        {
            return _context.Users
                .Where(x => x.Username == username)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Username = u.Username
                })
                .FirstOrDefault();
        }
        //21.06.2026
        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
 
