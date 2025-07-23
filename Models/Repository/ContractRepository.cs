using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

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
            var contracts = _context.TblContracts
               .Where(c => c.Id == id)

               .Include(c => c.TblLoans)
               .Include(c => c.User)
               .Include(c => c.TblPaymentHistories)

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
                   paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
                   {
                       id = ph.Id,
                       amount = (long)ph.Amount,
                       payment_day = (DateTime)ph.PaymentDay,
                       contract_id = (int)ph.ContractId
                   }).ToHashSet(),
                   total_paid = (long)c.TotalPaid,

                   next_payment_due = c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()),
                   left_day = (double)((c.StartDate.Value.AddYears(c.TblPaymentHistories.Count())) - DateTime.Now).TotalDays,
                   year_paid = (long)c.YearPaid,
                   number_year_paid = c.NumberYearPaid,
                   status = c.Status ?? 0
               }).FirstOrDefault();



            return contracts;
         
        }

        public HashSet<ContractView> FindByKeywork(string phone)
        {
            var query = from c in _context.TblContracts
                        join u in _context.TblUsers on c.UserId equals u.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                        join i in _context.Insurances on c.InsuranceId equals i.Id into insuranceGroup
                        from i in insuranceGroup.DefaultIfEmpty()
                        join emp in _context.TblUsers on c.EmployeeId equals emp.Id into employeeGroup
                        from emp in employeeGroup.DefaultIfEmpty()
                        select new ContractView
                        {
                            id = c.Id,
                            user_id = (int)c.UserId,
                            user = new User
                            {
                                id = u.Id,
                                full_name = u.FullName, // Assuming FullName is the correct property
                                email = u.Email,
                                phone = u.Phone
                            },
                            insurance_id = (int)c.InsuranceId,
                            insurance = new InsuranceView
                            {
                                id = i.Id,
                                name = i.Name,
                                description = i.Description,
                                value = i.Value ?? 0,
                                year_max = i.YearMax ?? 0,
                                ex_image = i.ExImage
                            },
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            value_contract = (long)c.ValueContract,
                            employee_id = emp != null ? emp.Id : 0, // Handle null case for employee
                            year_paid = (long)c.YearPaid,
                            number_year_paid = c.NumberYearPaid,
                            paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
                            {
                                id = ph.Id,
                                amount = (long)ph.Amount,
                                payment_day = (DateTime)ph.PaymentDay,
                                contract_id = (int)ph.ContractId
                            }).ToHashSet(),
                            next_payment_due = c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()),
                            employee = emp != null ? new User
                            {
                                id = emp.Id,
                                full_name = emp.FullName,
                                email = emp.Email,
                                phone = emp.Phone
                            } : null, // Handle null case for employee
                            status = c.Status ?? 0
                        };

            var contracts = query.Where(c => c.user.phone.Contains(phone)).ToHashSet();
            return contracts; // Ensure a return statement is present for all code paths
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
                        .Include(c => c.TblPaymentHistories)
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
                    total_paid = (long)c.TotalPaid,
                    number_year_paid = c.NumberYearPaid,
                    next_payment_due = c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()),
                    status = c.Status ?? 0
                }).ToHashSet();
            //var query = from c in _context.TblContracts
            //            where c.UserId == userId
            //            join u in _context.TblUsers on c.UserId equals u.Id into userGroup
            //            from u in userGroup.DefaultIfEmpty()
            //            join i in _context.Insurances on c.InsuranceId equals i.Id into insuranceGroup
            //            from i in insuranceGroup.DefaultIfEmpty()
            //            join emp in _context.TblUsers on c.EmployeeId equals emp.Id into employeeGroup
            //            from emp in employeeGroup.DefaultIfEmpty()
            //            join ph in _context.TblPaymentHistories on c.Id equals ph.ContractId into paymentGroup
            //            from ph in paymentGroup.DefaultIfEmpty()
            //            select new ContractView
            //            {
            //                id = c.Id,
            //                user_id = (int)c.UserId,
            //                user = new User
            //                {
            //                    id = u.Id,
            //                    full_name = u.FullName, // Assuming FullName is the correct property
            //                    email = u.Email,
            //                    phone = u.Phone
            //                },
            //                insurance_id = (int)c.InsuranceId,
            //                insurance = new InsuranceView
            //                {
            //                    id = i.Id,
            //                    name = i.Name,
            //                    description = i.Description,
            //                    value = i.Value ?? 0,
            //                    year_max = i.YearMax ?? 0,
            //                    ex_image = i.ExImage
            //                },
            //                StartDate = c.StartDate,
            //                EndDate = c.EndDate,
            //                value_contract = (long)c.ValueContract,
            //                employee_id = emp != null ? emp.Id : 0, // Handle null case for employee
            //                year_paid = (long)c.YearPaid,
            //                total_paid = (long)c.TotalPaid,
            //                paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
            //                {
            //                    id = ph.Id,
            //                    amount = (long)ph.Amount,
            //                    payment_day = (DateTime)ph.PaymentDay,
            //                    contract_id = (int)ph.ContractId,
            //                    sum_amount = c.TblPaymentHistories.Sum(ph => (long)ph.Amount)
            //                }).ToHashSet(),

            //                number_year_paid = c.NumberYearPaid,
            //                status = c.Status ?? 0
            //            };
            //var contracts = query.Where(c =>c.user_id == userId).ToHashSet();

            return contracts;
        }
        //public HashSet<ContractView> GetAll()
        //{

        //    var query  = from c in _context.TblContracts
        //                join u in _context.TblUsers on c.UserId equals u.Id into userGroup
        //                from u in userGroup.DefaultIfEmpty()
        //                join i in _context.Insurances on c.InsuranceId equals i.Id into insuranceGroup
        //                from i in insuranceGroup.DefaultIfEmpty()
        //                join emp in _context.TblUsers on c.EmployeeId equals emp.Id into employeeGroup
        //                from emp in employeeGroup.DefaultIfEmpty()
                       

        //                 select new ContractView
        //                {
        //                    id = c.Id,
        //                    user_id = (int)c.UserId,
        //                    user = new User
        //                    {
        //                        id = u.Id,
        //                        full_name = u.FullName, // Assuming FullName is the correct property
        //                        email = u.Email,
        //                        phone = u.Phone
        //                    },
        //                    insurance_id = (int)c.InsuranceId,
        //                    insurance = new InsuranceView
        //                    {
        //                        id = i.Id,
        //                        name = i.Name,
        //                        description = i.Description,
        //                        value = i.Value??0,
        //                        year_max = i.YearMax??0,
        //                        ex_image = i.ExImage
        //                    },
        //                    StartDate = c.StartDate,
        //                    EndDate = c.EndDate,
        //                    value_contract = (long)c.ValueContract,
        //                    employee_id = emp != null ? emp.Id : 0, // Handle null case for employee
        //                    year_paid = (long)c.YearPaid,
        //                    number_year_paid = c.NumberYearPaid,
        //                    paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
        //                    {
        //                        id = ph.Id,
        //                        amount = (long)ph.Amount,
        //                        payment_day = (DateTime)ph.PaymentDay,
        //                        contract_id = (int)ph.ContractId
        //                    }).ToHashSet(),
        //                    next_payment_due = c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()),
        //                     employee = emp != null ? new User
        //                    {
        //                        id = emp.Id,
        //                        full_name = emp.FullName,
        //                        email = emp.Email,
        //                        phone = emp.Phone
        //                    } : null, // Handle null case for employee
        //                    status = c.Status ?? 0
        //                };
        //    var contracts = query.ToHashSet();

        //    return contracts;



        //}

        public async Task< HashSet<ContractView>> GetAll()
        {

            var query = from c in _context.TblContracts
                        join u in _context.TblUsers on c.UserId equals u.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                        join i in _context.Insurances on c.InsuranceId equals i.Id into insuranceGroup
                        from i in insuranceGroup.DefaultIfEmpty()
                        join emp in _context.TblUsers on c.EmployeeId equals emp.Id into employeeGroup
                        from emp in employeeGroup.DefaultIfEmpty()


                        select new ContractView
                        {
                            id = c.Id,
                            user_id = (int)c.UserId,
                            user = new User
                            {
                                id = u.Id,
                                full_name = u.FullName, // Assuming FullName is the correct property
                                email = u.Email,
                                phone = u.Phone
                            },
                            insurance_id = (int)c.InsuranceId,
                            insurance = new InsuranceView
                            {
                                id = i.Id,
                                name = i.Name,
                                description = i.Description,
                                value = i.Value ?? 0,
                                year_max = i.YearMax ?? 0,
                                ex_image = i.ExImage
                            },
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            value_contract = (long)c.ValueContract,
                            employee_id = emp != null ? emp.Id : 0, // Handle null case for employee
                            year_paid = (long)c.YearPaid,
                            number_year_paid = c.NumberYearPaid,
                            paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
                            {
                                id = ph.Id,
                                amount = (long)ph.Amount,
                                payment_day = (DateTime)ph.PaymentDay,
                                contract_id = (int)ph.ContractId
                            }).ToHashSet(),
                            next_payment_due = c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()),
                            employee = emp != null ? new User
                            {
                                id = emp.Id,
                                full_name = emp.FullName,
                                email = emp.Email,
                                phone = emp.Phone
                            } : null, // Handle null case for employee
                            status = c.Status ?? 0
                        };
            var contracts = query.ToHashSet();

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
                TotalPaid = (entity.value_contract / entity.number_year_paid),
                Status = entity.status,
                EmployeeId = entity.employee_id
            };
            _context.TblContracts.Add(contract);
            _context.SaveChanges();
            return contract.Id; // Return the newly created contract ID
        }
        public HashSet<ContractView> GetByEmployeeId(int employeeId)
        {
            var contracts = _context.TblContracts
                .Where(c => c.EmployeeId == employeeId)
                
                .Include(c => c.TblLoans)
                .Include(c => c.User)   
                .Include(c => c.TblPaymentHistories)

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
                    paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
                    {
                        id = ph.Id,
                        amount = (long)ph.Amount,
                        payment_day = (DateTime)ph.PaymentDay,
                        contract_id = (int)ph.ContractId
                    }).ToHashSet(),

                    next_payment_due = c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()),
                    left_day =(double) ((c.StartDate.Value.AddYears(c.TblPaymentHistories.Count()))- DateTime.Now).TotalDays,
                    year_paid = (long)c.YearPaid,
                    number_year_paid = c.NumberYearPaid,
                    status = c.Status ?? 0
                }).ToHashSet();
            
            
            
            
            return contracts;
        }
        public async Task< HashSet<ContractView>>Test()
        {
            var contracts = await _context.TblContracts
     .Include(c => c.TblLoans)
     .Include(c => c.User)
     .Include(c => c.TblPaymentHistories)
     .Where(c => c.Status == 1 && c.StartDate != null)
     .ToListAsync();

            var contractViews = contracts.Select(c => new ContractView
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
            
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                value_contract = (long)c.ValueContract,
                employee_id = c.EmployeeId ?? 0,
                year_paid = (long)c.YearPaid,
                number_year_paid = c.NumberYearPaid,
                status = c.Status ?? 0,
                paymentHistories = c.TblPaymentHistories.Select(ph => new PaymentHistory
                {
                    id = ph.Id,
                    amount = (long)ph.Amount,

                    payment_day = (DateTime)ph.PaymentDay,
                    contract_id = (int)ph.ContractId
                }).ToHashSet()
            }).ToHashSet();
           
                return contractViews;

        }

        HashSet<ContractView> IRepository<ContractView>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
