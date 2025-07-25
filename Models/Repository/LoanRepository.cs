using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Microsoft.EntityFrameworkCore;

namespace Insurance_agency.Models.Repository
{
    public class LoanRepository : IRepository<Loan>
    {
        private static LoanRepository _instance;
        private readonly InsuranceContext _context;
        private LoanRepository()
        {
            _context = new InsuranceContext();
        }
        public static LoanRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoanRepository();
                }
                return _instance;
            }
        }
        public bool Create(Loan entity)
        {
        


            try
            {
                var loan = new TblLoan
                {
                    ContractId = entity.ContractId,
                    LoanAmount = entity.LoanAmount,
                    RequestDate = entity.RequestDate,
                    Status = entity.Status
                };
                _context.TblLoans.Add(loan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return false;
            }



        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Loan FindById(int id)
        {
           
            var loan = _context.TblLoans
                .Include(l => l.Contract)
                .Where(l => l.Id == id)
                .Select(l => new Loan
                {
                    Id = l.Id,
                    ContractId = l.ContractId,
                    LoanAmount = l.LoanAmount,
                    RequestDate = l.RequestDate,
                    Status = l.Status,
                    Contract = new ContractView
                    {
                        id = l.Contract.Id,
                        user_id = l.Contract.UserId,
                        user = new User
                        {
                            id = l.Contract.User.Id,
                            email = l.Contract.User.Email,
                            full_name = l.Contract.User.FullName,
                            phone = l.Contract.User.Phone,
                            address = l.Contract.User.Address
                        },
                        employee_id = (int)l.Contract.EmployeeId,
                        StartDate =(DateTime) l.Contract.StartDate,
                        EndDate = (DateTime)l.Contract.EndDate,
                        year_paid = (int)l.Contract.YearPaid,
                    }
                }).FirstOrDefault();
            return loan;
        }

        public HashSet<Loan> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<Loan> GetAll()
        {
           var loans = _context.TblLoans
                .Include(loan => loan.Contract)
                .Select(loan => new Loan
                {
                    Id = loan.Id,
                    ContractId = loan.ContractId,
                    LoanAmount = loan.LoanAmount,
                    RequestDate = loan.RequestDate,
                    Contract = new ContractView
                    {
                        
                     
                        id = loan.Contract.Id,
                        user_id = loan.Contract.UserId,
                        employee_id= (int)loan.Contract.EmployeeId,
                        StartDate =(DateTime) loan.Contract.StartDate,
                        EndDate = (DateTime)loan.Contract.EndDate,
                        year_paid = (int)loan.Contract.YearPaid,
                    },
                    Status = loan.Status
                }).ToHashSet();
            return loans;
        }

        public bool Update(Loan entity)
        {
          
            try
            {
                var loan = _context.TblLoans.Find(entity.Id);
                if (loan == null)
                {
                    return false; // Loan not found
                }
                loan.LoanAmount = entity.LoanAmount;
                loan.RequestDate = entity.RequestDate;
                loan.Status = entity.Status;
                _context.TblLoans.Update(loan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return false;
            }
        }
        public HashSet<Loan> GetAllByEmployeeId(int employeeId)
        {
            var loans = _context.TblLoans
                .Include(loan => loan.Contract)
                .Where(loan => loan.Contract.EmployeeId == employeeId)
                .Select(loan => new Loan
                {
                    Id = loan.Id,
                    ContractId = loan.ContractId,
                    LoanAmount = loan.LoanAmount,
                    RequestDate = loan.RequestDate,
                    Contract = new ContractView
                    {
                        id = loan.Contract.Id,
                        user_id = loan.Contract.UserId,
                        employee_id= (int)loan.Contract.EmployeeId,
                        StartDate =(DateTime) loan.Contract.StartDate,
                        EndDate = (DateTime)loan.Contract.EndDate,
                        year_paid = (int)loan.Contract.YearPaid,
                    },
                    Status = loan.Status
                }).ToHashSet();
            return loans;
        }
    }
}
