using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class ContractRepository : IRepository<ContractView>
    {
        private static ContractRepository _instance;

        private InsuranceContext _context;
        public static ContractRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContractRepository();
                }
                return _instance;
            }
        }
        private ContractRepository()
        {
            // Initialize any resources if needed

            _context = new InsuranceContext();
        }
        public bool Create(ContractView entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ContractView entity)
        {
            throw new NotImplementedException();
        }

        public ContractView FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<ContractView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<ContractView> GetAll()
        {

            var contracts = _context.TblContracts
                .Select(c => new ContractView
                {
                    id = c.Id,
                    user_id = c.UserId,
                    insurance_id = c.InsuranceId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    value_contract =(long) c.ValueContract,
                    year_paid = (long)c.YearPaid,
                    number_year_paid = c.NumberYearPaid,
                    status = c.Status
                }).ToHashSet();
            return contracts;

        }

        public bool Update(ContractView entity)
        {
            throw new NotImplementedException();
        }
    }
}
