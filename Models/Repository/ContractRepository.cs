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

            var contract = new TblContract
            {
                UserId = entity.user_id,
                InsuranceId = entity.insurance_id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ValueContract = entity.value_contract,
                YearPaid = entity.year_paid,
                NumberYearPaid = entity.number_year_paid,
                TotalPaid = entity.total_paid,
                Status = entity.status,
                EmployeeId = entity.employee_id
            };
            _context.TblContracts.Add(contract);
            return _context.SaveChanges() > 0;
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
            //var query = from c in _context.TblContracts
            //            join u in _context.TblUsers on c.UserId equals u.Id
            //            join i in _context.Insurances on c.InsuranceId equals i.Id
            //            where c.Id == id
            //            select new ContractView
            //            {
            //                id = c.Id,
            //                user_id = (int)c.UserId,
            //                insurance_id = (int)c.InsuranceId,
            //                StartDate = c.StartDate,
            //                EndDate = c.EndDate,
            //                value_contract = (long)c.ValueContract,
            //                total_paid = (long)c.TotalPaid,
            //                year_paid = (long)c.YearPaid,
            //                user = new User
            //                {
            //                    id = u.Id,
            //                    full_name = u.Password,
            //                    email = u.Email,
            //                    phone = u.Phone
            //                },
            //                insurance = new InsuranceView
            //                {
            //                    id = i.Id,
            //                    name = i.Name,
            //                    description = i.Description,
            //                    value = (int)i.Value,
            //                    year_max = (int)i.YearMax,

            //                    ex_image = i.ExImage,

            //                },
            //                number_year_paid = c.NumberYearPaid,
            //                status = (int)c.Status

            //            };
            var query = from c in _context.TblContracts
                        join u in _context.TblUsers on c.UserId equals u.Id
                        join i in _context.Insurances on c.InsuranceId equals i.Id
                        where c.Id == id  
                        select new ContractView
                        {
                            id = c.Id,
                            user_id = (int)c.UserId,
                            insurance_id = (int)c.InsuranceId,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            value_contract = (long)c.ValueContract,
                            total_paid = (long)c.TotalPaid,
                            year_paid = (long)c.YearPaid,
                            user = new User
                            {
                                id = u.Id,
                                full_name = u.FullName, // Assuming FullName is the correct property
                                email = u.Email,
                                phone = u.Phone
                            },
                            insurance = new InsuranceView
                            {
                                id = i.Id,
                                name = i.Name,
                                description = i.Description,
                                value = (int)i.Value,
                                year_max = (int)i.YearMax,
                                ex_image = i.ExImage
                            },
                            number_year_paid = c.NumberYearPaid,
                            status = (int)c.Status
                        };

            var contract = query.FirstOrDefault();
            return contract;
        }
        public ContractView getById(int id)
        {

            var query = from c in _context.TblContracts
                        where c.Id == id
                        select new ContractView
                        {
                            id = c.Id,
                            user_id = (int)c.UserId,
                            insurance_id = (int)c.InsuranceId,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            value_contract = (long)c.ValueContract,
                            total_paid = (long)c.TotalPaid,
                            year_paid = (long)c.YearPaid,
                            number_year_paid = c.NumberYearPaid,
                            status = (int)c.Status
                        };

            var contract = query.FirstOrDefault();
            return contract;
        }

        public HashSet<ContractView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }


        public bool Update(ContractView entity)
        {

            var contract = _context.TblContracts.FirstOrDefault(c => c.Id == entity.id);
            if (contract == null)
            {
                return false; // Contract not found
            }
            contract.UserId = entity.user_id;
            contract.InsuranceId = entity.insurance_id;
            contract.StartDate = entity.StartDate;
            contract.EndDate = entity.EndDate;
            contract.ValueContract = entity.value_contract;
            contract.YearPaid = entity.year_paid;
            contract.NumberYearPaid = entity.number_year_paid;
            contract.TotalPaid = entity.total_paid;

            contract.Status = entity.status;
            contract.EmployeeId = entity.employee_id;
            return _context.SaveChanges() > 0;
        }
        public HashSet<ContractView> FindByEmployeeId(int employeeId)
        {
            var contracts = _context.TblContracts
                .Where(c => c.EmployeeId == employeeId)
                .Select(c => new ContractView
                {
                    id = c.Id,
                    user_id = (int)c.UserId,
                    insurance_id = (int)c.InsuranceId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    value_contract = (long)c.ValueContract,
                    year_paid = (long)c.YearPaid,
                    employee_id = c.EmployeeId ?? 0,
                    number_year_paid = c.NumberYearPaid,
                    status = (int)c.Status
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
        user_id = (int)c.UserId,
        insurance_id = (int)c.InsuranceId,
        StartDate = c.StartDate,
        EndDate = c.EndDate,
        value_contract = (long)c.ValueContract,
        year_paid = (long)c.YearPaid,
        number_year_paid = c.NumberYearPaid,
        status = (int)c.Status
    }).ToHashSet();
            return contracts;
        }
        public HashSet<ContractView> GetContractsByUserId(int userId)
        {
            var contracts = _context.TblContracts
                .Where(c => c.UserId == userId)
                .Select(c => new ContractView
                {
                    id = c.Id,
                    user_id = (int)c.UserId,
                    insurance_id = (int)c.InsuranceId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    value_contract = (long)c.ValueContract,
                    employee_id = (int)c.EmployeeId,
                    year_paid = (long)c.YearPaid,
                    number_year_paid = c.NumberYearPaid,
                    status = c.Status??0
                }).ToHashSet();
            return contracts;
        }
        public HashSet<ContractView> GetAll()
        {

            var contracts = new HashSet<ContractView>();
                contracts = _context.TblContracts
                .Select(c => new ContractView
                {
                    id = c.Id,
                    user_id = (int)c.UserId,
                    user = new User
                    {
                        id = c.User.Id,
                        full_name = c.User.FullName, // Assuming FullName is the correct property
                        email = c.User.Email,
                        phone = c.User.Phone
                    },
                    insurance_id = (int)c.InsuranceId,
                    insurance = new InsuranceView
                    {
                        id = c.Insurance.Id,
                        name = c.Insurance.Name,
                        description = c.Insurance.Description,
                        value = c.Insurance.Value ?? 0,
                        year_max = c.Insurance.YearMax ?? 0,
                        ex_image = c.Insurance.ExImage
                    },
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    value_contract = (long)c.ValueContract,
                    employee_id = c.EmployeeId ?? 0,


                    year_paid = (long)c.YearPaid,
                    number_year_paid = c.NumberYearPaid,
                    status = c.Status ?? 0
                }).ToHashSet();
            // retrive employee information if available
            foreach (var contract in contracts)
            {
                if (contract.employee_id != 0)
                {
                    var employee = _context.TblUsers.FirstOrDefault(u => u.Id == contract.employee_id);
                    if (employee != null)
                    {
                        contract.employee = new User
                        {
                            id = employee.Id,
                            full_name = employee.FullName,
                            email = employee.Email,
                            phone = employee.Phone
                        };
                    }
                }
            }

            return contracts;


        }

        public int CreateReturnId(ContractView entity)
        {
            var contract = new TblContract
            {
                UserId = entity.user_id,
                InsuranceId = entity.insurance_id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ValueContract = entity.value_contract,
                YearPaid = entity.year_paid,
                NumberYearPaid = entity.number_year_paid,
                TotalPaid = (entity.value_contract/entity.number_year_paid),
                Status = entity.status,
                EmployeeId = entity.employee_id
            };
            _context.TblContracts.Add(contract);
            _context.SaveChanges();
            return contract.Id; // Return the newly created contract ID
        }
    }
}
