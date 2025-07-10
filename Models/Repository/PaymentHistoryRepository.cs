using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class PaymentHistoryRepository:IRepository<PaymentHistory>
    {
        private static PaymentHistoryRepository _instance = null;
        public InsuranceContext _context;
        private PaymentHistoryRepository() { 
            _context = new InsuranceContext();
        }
        public static PaymentHistoryRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaymentHistoryRepository();
                }
                return _instance;
            }
        }

        public bool Create(PaymentHistory entity)
        {
            try
            {
                if (entity != null)
                {
                    TblPaymentHistory history = new TblPaymentHistory
                    {
                        Id = entity.id,
                        Amount = entity.amount,
                        ContractId = entity.contract_id,
                        Status = 1,
                        PaymentDay = DateTime.Now,
                    };
                    _context.Add(history);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PaymentHistory FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<PaymentHistory> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<PaymentHistory> Get(int id=0)
        {
            try
            {
                var item = _context.TblPaymentHistories.Where(d => d.ContractId == id).Select(d => new PaymentHistory
                {
                    id = d.Id,
                    contract_id = (int)d.ContractId,
                    amount = (long)d.Amount,
                    payment_day = (DateTime)d.PaymentDay,
                    Status = (int)d.Status,
                    //Contract = ContractRepository.Instance.FindById((int)d.ContractId)
                }).ToHashSet();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception e)
            {
            }
            return new HashSet<PaymentHistory>();
        }
        public HashSet<PaymentHistory> GetByUserId(int id = 0)
        {
            try
            {
                var query = from ph in _context.TblPaymentHistories
                            join c in _context.TblContracts on ph.ContractId equals c.Id
                            where c.UserId == id
                            select new PaymentHistory
                            {
                                id = ph.Id,
                                contract_id = (int)ph.ContractId,
                                amount = (long)ph.Amount,
                                payment_day = (DateTime)ph.PaymentDay,
                                Status = (int)ph.Status,
                                //Contract = ContractRepository.Instance.FindById((int)d.ContractId)
                            };
                var item = query.ToHashSet();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception e)
            {
            }
            return new HashSet<PaymentHistory>();
        }


        public HashSet<PaymentHistory> GetAll()
        {
            try
            {
                var item = _context.TblPaymentHistories.Select(d=> new PaymentHistory
                {
                    id = d.Id,
                    contract_id = (int)d.ContractId,
                    amount = (long)d.Amount,
                    payment_day = (DateTime)d.PaymentDay,
                    Status = (int)d.Status,
                    Contract = ContractRepository.Instance.FindById((int)d.ContractId)
                }).ToHashSet();
                if(item != null)
                {
                    return item;
                }
            }
            catch (Exception e)
            {
            }
            return new HashSet<PaymentHistory>();
        }

        public bool Update(PaymentHistory entity)
        {
            throw new NotImplementedException();
        }
    }

}
