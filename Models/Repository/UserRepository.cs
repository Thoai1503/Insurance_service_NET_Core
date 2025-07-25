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
            try
            {
                if (entity != null)
                {
                    var item = new TblUser
                    {
                        Email = entity.email,
                        FullName = entity.full_name,
                        Active = 1,
                        Address = entity.address,
                        Password = entity.password,
                        Phone = entity.phone,
                        AuthId = 4,
                        Avatar = entity.avatar,
                    };
                    _context.Add(item);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
        public bool Update(User entity)
        {
           
            try
            {
                if (entity != null)
                {
                    var user = _context.TblUsers.FirstOrDefault(u => u.Id == entity.id);
                    if (user != null)
                    {
                        //user.FullName = entity.full_name;
                        //user.Email = entity.email;
                        user.Phone = entity.phone;
                        user.Address = entity.address;
                        //user.AuthId = entity.auth_id;
                        user.Avatar = entity.avatar;
                        //user.Active = entity.active;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.Message);
            }
            return false;
        }
        public bool Update1(User entity)
        {

            try
            {
                if (entity != null)
                {
                    var user = _context.TblUsers.Where(d=>d.Id==entity.id).FirstOrDefault();
                    if (user != null)
                    {
                        //user.FullName = entity.full_name;
                        //user.Email = entity.email;
                        user.Phone = entity.phone;
                        user.Address = entity.address;
                        //user.AuthId = entity.auth_id;
                        if(entity.avatar != "user.jpg")
                        {
                            user.Avatar = entity.avatar;
                        }
                        //user.Active = entity.active;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.Message);
            }
            return false;
        }
        public bool checkEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    var result = _context.TblUsers.Any(d=>d.Email == email);
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return true;
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
                avatar = u.Avatar,
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
                active =(int) u.Active,
                created_date = u.CreatedDate,
                avatar = u.Avatar


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
                avatar = u.Avatar,
                created_date = u.CreatedDate,
                active = (int)u.Active
            }).ToHashSet();
            if (user == null)
            {
                throw new Exception("No employee user found.");
            }
            return user;
        }
        public bool CreateEmployee(User entity)
        {
            try
            {
                if (entity != null)
                {
                    var user = new TblUser
                    {
                        FullName = entity.full_name,
                        Email = entity.email,
                        Password = "default123",
                        Phone = entity.phone,
                        Address = entity.address,
                        AuthId = 3,
                        Avatar = entity.avatar,
                        Active = 1 ,// Assuming 1 means active
                        CreatedDate = DateTime.Now
                    };
                    _context.TblUsers.Add(user);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating employee: " + ex.Message);
            }
            return false;
        }
    }

}
