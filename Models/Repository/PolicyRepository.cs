using Insurance_agency.Models.Entities;

namespace Insurance_agency.Models.Repository
{
    public class PolicyRepository : IRepository<Policy>
    {
        private readonly InsuranceContext _context;
        //create singleton instance of PolicyRepository
        private static PolicyRepository? _instance;
        public static PolicyRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PolicyRepository();
                }
                return _instance;
            }
        }
        private PolicyRepository()
        {
            _context = new InsuranceContext();
        }

        public bool Create(Policy entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Policy entity)
        {
            throw new NotImplementedException();
        }
        public Policy FindById(int id)
        {
            throw new NotImplementedException();
        }
        public HashSet<Policy> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }
        public HashSet<Policy> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Policy entity)
        {
            throw new NotImplementedException();
        }
    }

}
