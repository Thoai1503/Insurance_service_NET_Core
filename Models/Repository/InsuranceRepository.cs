using Insurance_agency.Models.Entity;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class InsuranceRepository : IRepository<InsuranceView>
    {
        private static InsuranceRepository _instance = null;
        private InsuranceRepository() {
            _context = new InsuranceContext();
        }
        public static InsuranceRepository Instance {  
            get {
                if(_instance==null)
                {
                    _instance = new InsuranceRepository();
                }
                return _instance; 
            } 
        }
        public InsuranceContext _context;
        public bool Create(InsuranceView entity)
        {
            try
            {
                if (entity != null)
                {
                    var Insurance = new Insurance
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        InsuranceTypeId = entity.InsuranceTypeId,
                        TargetId = entity.TargetId,
                        ExImage = entity.ExImage,
                    };
                    var q = _context.Add(Insurance);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool Delete(InsuranceView entity)
        {
            throw new NotImplementedException();
        }

        public InsuranceView FindById(int id)
        {

            try
            {
                var item = _context.Insurances.Where(d => d.Id == id).Select(d => new InsuranceView
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    InsuranceTypeId = (int)d.InsuranceTypeId,
                    TargetId = (int)d.TargetId,
                    ExImage = d.ExImage
                }).FirstOrDefault();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public HashSet<InsuranceView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<InsuranceView> GetAll()
        {
            try
            {
                var item = _context.Insurances.Select(d=>new InsuranceView
                {
                    Id = d.Id,
                    Name =d.Name,
                    Description = d.Description,
                    InsuranceTypeId = (int)d.InsuranceTypeId,
                    TargetId = (int)d.TargetId,
                    ExImage = d.ExImage
                }).ToHashSet();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {
            }
            return new HashSet<InsuranceView>();
        }

        public bool Update(InsuranceView entity)
        {
            throw new NotImplementedException();
        }
    }
}
