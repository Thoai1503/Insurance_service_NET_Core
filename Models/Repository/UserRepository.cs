using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly InsuranceContext _context;
        //create singleton instance of UserRepository
        private static UserRepository? _instance;
        public static UserRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserRepository();
                }
                return _instance;
            }
        }
        private UserRepository()
        {
            _context = new InsuranceContext();
        }
        public bool Create(User entity)
        {
            throw new NotImplementedException();
        }
        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public HashSet<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public User FindById(int id)
        {
           // throw new NotImplementedException();

            var user = _context.TblUsers.Where(u => u.Id == id).Select(u => new User
            {
                id = u.Id,
                full_name = u.FullName,
                email = u.Email,
                phone = u.Phone,
                auth_id = (int)u.AuthId,
                address = u.Address,
                active = (int)u.Active
            }).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }
        public HashSet<User> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }
        public HashSet<User> GetCustomerUser()
        {
            var user = _context.TblUsers.Where(u => u.AuthId == 4).Select(u => new User
            {
                id = u.Id,
                full_name = u.FullName,
                email = u.Email,
                phone = u.Phone,
                auth_id = (int)u.AuthId,
                address = u.Address,
                active =(int) u.Active
                
            }).ToHashSet();
            if (user == null)
            {
                throw new Exception("No customer user found.");
            }
            return user;
        }
        public HashSet<User> GetAllEmployeeUser()
        {
            var user = _context.TblUsers.Where(u => u.AuthId == 3).Select(u => new User
            {
                id = u.Id,
                full_name = u.FullName,
                email = u.Email,
                phone = u.Phone,
                auth_id = (int)u.AuthId,
                address = u.Address,
                active = (int)u.Active
            }).ToHashSet();
            if (user == null)
            {
                throw new Exception("No employee user found.");
            }
            return user;
        }
    }

}
