using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Microsoft.EntityFrameworkCore;

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

            try
            {
                var policy = new TblPolicy
                {
                    Name = entity.name,
                    AgeMin = entity.age_min,
                    AgeMax = entity.age_max,
                    Description = entity.description,
                    Active = entity.active,
                    InsuranceId = entity.insurance_id
                };
                _context.TblPolicies.Add(policy);
                _context.SaveChanges();
                return true;
            }
            catch (Exception )
            {
                // Handle exception (log it, rethrow it, etc.)
                return false;
            }

        }
        public bool Delete(int id)
        {

            try
            {
                var policy = _context.TblPolicies.Find(id);
                if (policy != null)
                {
                    _context.TblPolicies.Remove(policy);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception )
            {
                // Handle exception (log it, rethrow it, etc.)
                return false;
            }
        }
        public Policy? FindById(int id)
        {
            try
            {
                var poli = _context.TblPolicies.Include(p => p.Insurance).FirstOrDefault(p => p.Id == id);
                if (poli == null) return null;
                return new Policy
                {
                    id = poli.Id,
                    name = poli.Name ?? string.Empty,
                    description = poli.Description ?? string.Empty,
                    age_min = poli.AgeMin ?? 0,
                    age_max = poli.AgeMax ?? 0,
                    active = poli.Active ?? 0,
                    insurance_id = poli.InsuranceId ?? 0,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public HashSet<Policy> FindByKeywork(string keywork)
        {
            try
            {
                return _context.TblPolicies.Where(p => p.Name != null && p.Name.Contains(keywork))
                .Select(p => new Policy
                {
                    id = p.Id,
                    name = p.Name ?? string.Empty,
                    description = p.Description ?? string.Empty,
                    active = p.Active ?? 0,
                    age_max = p.AgeMax ?? 0,
                    age_min = p.AgeMin ?? 0,
                    insurance_id = p.InsuranceId ?? 0,

                }).ToHashSet();
            }
            catch
            {
                return new HashSet<Policy>();
            }
        }
        public HashSet<Policy> GetAll()
        {
            try
            {
                return _context.TblPolicies.Include(p => p.Insurance).Select(p => new Policy
                {
                    id = p.Id,
                    name = p.Name ?? string.Empty,
                    description = p.Description ?? string.Empty,
                    active = p.Active ?? 0,
                    age_min = p.AgeMin ?? 0,
                    age_max = p.AgeMax ?? 0,
                    insurance_id = p.InsuranceId ?? 0,
                }).ToHashSet();
            }
            catch
            {
                return new HashSet<Policy>();
            }
        }

        public bool Update(Policy entity)
        {
            try
            {
                var poli = _context.TblPolicies.Find(entity.id);
                if (poli == null) return false;
                poli.Name = entity.name;
                poli.Description = entity.description;
                poli.Active = entity.active;
                poli.AgeMin = entity.age_min;
                poli.AgeMax = entity.age_max;
                poli.InsuranceId = entity.insurance_id;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public HashSet<Policy> GetAllByInsuranceId(int insuranceId)
        {
            try
            {
                return _context.TblPolicies.Where(p => p.InsuranceId == insuranceId).Select(p => new Policy
                {
                    id = p.Id,
                    name = p.Name ?? string.Empty,
                    description = p.Description ?? string.Empty,
                    age_min = p.AgeMin ?? 0,
                    age_max = p.AgeMax ?? 0,
                    active = p.Active ?? 0,
                    insurance_id = p.InsuranceId ?? 0,

                }).ToHashSet();
            }
            catch
            {
                // Handle exception (log it, rethrow it, etc.)
                return new HashSet<Policy>();
            }
        }
        public HashSet<Policy> FindByNameInsurance(string keyword)
        {
            try
            {
                return _context.TblPolicies
                    .Include(p => p.Insurance)
                    .Where(p => p.Insurance != null && p.Insurance.Name.Contains(keyword))
                    .Select(p => new Policy
                    {
                        id = p.Id,
                        name = p.Name ?? string.Empty,
                        description = p.Description ?? string.Empty,
                        active = p.Active ?? 0,
                        age_max = p.AgeMax ?? 0,
                        age_min = p.AgeMin ?? 0,
                    }).ToHashSet();
                ;

            }
            catch
            {
                return new HashSet<Policy>();
            }

        }
    }
}


