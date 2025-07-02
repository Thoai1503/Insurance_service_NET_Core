using Insurance_agency.Models.Entities;

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
        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }
        public HashSet<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public User FindById(int id)
        {
            throw new NotImplementedException();
        }
        public HashSet<User> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }
    }

}
