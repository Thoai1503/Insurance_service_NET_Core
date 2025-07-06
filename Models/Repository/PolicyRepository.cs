using Insurance_agency.Models.Entity;
using Insurance_agency.Models.ModelView;

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
        public HashSet<Policy> GetAllByInsuranceId(int insuranceId)
        {
            try
            {
                 return _context.TblPolicies.Where(p => p.InsuranceId == insuranceId).Select(p => new Policy
                 {
                     id = p.Id,
                     name = p.Name,
                     description = p.Description ?? string.Empty,
                     age_min = p.AgeMin ?? 0,
                     age_max = p.AgeMax ?? 0,
                     active = p.Active ?? 0,
                     insurance_id = (int)p.InsuranceId ,

                 }).ToHashSet();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                return new HashSet<Policy>();
            }
        }
    }

}
