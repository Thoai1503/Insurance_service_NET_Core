using Insurance_agency.Models.Entities;

namespace Insurance_agency.Models.Repository
{
    public class InsuranceTypeRepository : IRepository<InsuranceTypeView>
    {
        private readonly InsuranceContext _context;
        //create singleton instance of InsuranceTypeRepository
        private static InsuranceTypeRepository? _instance;
        public static InsuranceTypeRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InsuranceTypeRepository();
                }
                return _instance;
            }
        }
        private InsuranceTypeRepository()
        {
            _context = new InsuranceContext();
        }
        public bool Create(InsuranceTypeView entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(InsuranceTypeView entity)
        {
            throw new NotImplementedException();
        }

        public InsuranceTypeView FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<InsuranceTypeView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }
        public HashSet<InsuranceTypeView> GetAll()
        {
            var allTypes = _context.TblInsuranceTypes
                .Where(x => x.Active == 1)
                .ToList();

            var childrenLookup = allTypes
                .Where(x => x.ParentId != 0)
                .GroupBy(x => x.ParentId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = allTypes
                .Where(x => x.ParentId == 0)
                .Select(x => new InsuranceTypeView
                {
                    id = x.Id,
                    name = x.Name,
                    description = x.Description,
                    parent_id = x.ParentId,
                    active = x.Active,
                    children = childrenLookup.ContainsKey(x.Id)
                        ? childrenLookup[x.Id].Select(c => new InsuranceTypeView
                        {
                            id = c.Id,
                            name = c.Name,
                            description = c.Description,
                            parent_id = c.ParentId,
                            active = c.Active,
                            children = new HashSet<InsuranceTypeView>()
                        }).ToHashSet()
                        : new HashSet<InsuranceTypeView>()
                })
                .ToHashSet();

            return result;
        }


        public bool Update(InsuranceTypeView entity)
        {
            throw new NotImplementedException();
        }
    }
}
