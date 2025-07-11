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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ContractView FindById(int id)
        {

            //var contract = _context.TblContracts
            //    .Where(c => c.Id == id)
            //    .Select(c => new ContractView
            //    {
            //        id = c.Id,
            //        user_id = c.UserId,
            //        insurance_id = c.InsuranceId,
            //        StartDate = c.StartDate,
            //        EndDate = c.EndDate,
            //        value_contract = (long)c.ValueContract,
            //        year_paid = (long)c.YearPaid,
            //        number_year_paid = c.NumberYearPaid,
            //        status = c.Status
            //    }).FirstOrDefault();
            var query = from c in _context.TblContracts
                        join u in _context.TblUsers on c.UserId equals u.Id
                        join i in _context.Insurances on c.InsuranceId equals i.Id
                        where c.Id == id
                        select new ContractView
                        {
                            id = c.Id,
                            user_id = c.UserId,
                            insurance_id = c.InsuranceId,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            value_contract = (long)c.ValueContract,
                            year_paid = (long)c.YearPaid,
                            user = new User
                            {
                                id = u.Id,
                                full_name = u.Password,
                                email = u.Email,
                                phone = u.Phone
                            },
                            insurance = new InsuranceView
                            {
                                id = i.Id,
                                name = i.Name,
                                description = i.Description,
                                ex_image = i.ExImage,

                            },
                            number_year_paid = c.NumberYearPaid,
                            status = c.Status
                        };
            var contract = query.FirstOrDefault();
            return contract;
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
                    value_contract = (long)c.ValueContract,
                    employee_id= c.EmployeeId ?? 0,
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
        public HashSet<ContractView> FindByEmployeeId(int employeeId)
        {
            var contracts = _context.TblContracts
                .Where(c => c.EmployeeId == employeeId)
                .Select(c => new ContractView
                {
                    id = c.Id,
                    user_id = c.UserId,
                    insurance_id = c.InsuranceId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    value_contract = (long)c.ValueContract,
                    year_paid = (long)c.YearPaid,
                    employee_id = c.EmployeeId ?? 0,
                    number_year_paid = c.NumberYearPaid,
                    status = c.Status
                }).ToHashSet();
            return contracts;
        }
        public HashSet<ContractView> GetInassignContracts()
        {
            var contracts = _context.TblContracts
    .Where(c => c.EmployeeId == 0)
    .Select(c => new ContractView
    {
        id = c.Id,
        user_id = c.UserId,
        insurance_id = c.InsuranceId,
        StartDate = c.StartDate,
        EndDate = c.EndDate,
        value_contract = (long)c.ValueContract,
        year_paid = (long)c.YearPaid,
        number_year_paid = c.NumberYearPaid,
        status = c.Status
    }).ToHashSet();
            return contracts;
        }
    }
}
