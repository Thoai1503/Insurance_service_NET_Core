using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public InsuranceTypeView FindById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var item = _context.TblInsuranceTypes.Where(d=>d.Id == id).Select(d=>new InsuranceTypeView
                    {
                        id = d.Id,
                        name = d.Name,
                        active = d.Active,
                        description = d.Description,
                        parent_id = id,
                    }).FirstOrDefault();
                    return item;
                }
            }
            catch (Exception ex)
            {
            }
            return new InsuranceTypeView();
        }

        public HashSet<InsuranceTypeView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }
        public HashSet<InsuranceTypeView> GetAll()
        {
            var allTypes = _context.TblInsuranceTypes
                 .Where(x => x.Active == 1)
                 .Select(c=> new InsuranceTypeView
                 {
                         id =c.Id,
                         name = c.Name,
                     description = c.Description,
                     parent_id = c.ParentId,
                     active = c.Active,
                     
                 }).ToHashSet();

            return allTypes;
           


        }


        public bool Update(InsuranceTypeView entity)
        {
            throw new NotImplementedException();
        }
    }
}
