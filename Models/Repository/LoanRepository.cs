using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

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
            throw new NotImplementedException();
        }

        public HashSet<Loan> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<Loan> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Loan entity)
        {
            throw new NotImplementedException();
        }
    }
}
