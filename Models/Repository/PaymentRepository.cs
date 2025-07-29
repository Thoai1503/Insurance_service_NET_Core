using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class PaymentRepository : IRepository<PaymentHistory>
    {
        private readonly InsuranceContext _context;
        //create singleton instance of InsuranceTypeRepository
        private static PaymentRepository? _instance;
        public static PaymentRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaymentRepository();
                }
                return _instance;
            }
        }

        private PaymentRepository()
        {
            _context = new InsuranceContext();
        }
        public bool Create(PaymentHistory entity)
        {
            try
            {
                var payment = new TblPaymentHistory
                {
                    ContractId = entity.contract_id,
                    Amount = entity.amount,
                    PaymentDay = entity.payment_day,
                    Status = entity.status
                };
                _context.TblPaymentHistories.Add(payment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return false;
            }

        }
        public long GetEarning(int month)
        {
            long result = 0;
            try
            {
                if (month <= 12)
                {
                    var item = _context.TblPaymentHistories.Where(d => (d.PaymentDay.Month == month))
                        .Select(d => new PaymentHistory
                        {
                            id = d.Id,
                            amount = (int)d.Amount,
                        }).ToHashSet();
                    foreach (var c in item)
                    {
                        result += c.amount;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
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

        public HashSet<PaymentHistory> GetAll(int month = 0, int client = 0)
        {
            try
            {
                var item = new HashSet<PaymentHistory>();
                if (client == 0)
                {
                    var query = from p in _context.TblPaymentHistories
                                join c in _context.TblContracts on p.ContractId equals c.Id
                                join u in _context.TblUsers on c.UserId equals u.Id
                                select new PaymentHistory
                                {
                                    id = p.Id,
                                    amount = (int)p.Amount,
                                    contract_id = (int)p.ContractId,
                                    payment_day = p.PaymentDay,
                                    Contract = new ContractView
                                    {
                                        id = c.Id,
                                        user_id = (int)c.UserId,
                                        user = new User
                                        {
                                            full_name = u.FullName
                                        }
                                    },
                                    status = (int)p.Status,
                                };
                    if (month == 0)
                    {
                        item = query.ToHashSet();
                    }
                    else if (month > 0 && month < 12)
                    {
                        item = query.Where(d => d.payment_day.Month == month).ToHashSet();
                    }
                }
                else
                {
                    if (month == 0)
                    {
                        var query = from p in _context.TblPaymentHistories
                                    join c in _context.TblContracts on p.ContractId equals c.Id
                                    join u in _context.TblUsers on c.UserId equals u.Id
                                    where u.Id == client
                                    select new PaymentHistory
                                    {
                                        id = p.Id,
                                        amount = (int)p.Amount,
                                        contract_id = (int)p.ContractId,
                                        payment_day = p.PaymentDay,
                                        Contract = new ContractView
                                        {
                                            id = c.Id,
                                            user_id = (int)c.UserId,
                                            user = new User
                                            {
                                                full_name = u.FullName
                                            }
                                        },
                                        status = (int)p.Status,
                                    };
                        item = query.ToHashSet();
                    }
                    else
                    {
                        var query = from p in _context.TblPaymentHistories
                                    join c in _context.TblContracts on p.ContractId equals c.Id
                                    join u in _context.TblUsers on c.UserId equals u.Id
                                    where p.PaymentDay.Month == month &&u.Id==client
                                    select new PaymentHistory
                                    {
                                        id = p.Id,
                                        amount = (int)p.Amount,
                                        contract_id = (int)p.ContractId,
                                        payment_day = p.PaymentDay,
                                        Contract = new ContractView
                                        {
                                            id = c.Id,
                                            user_id = (int)c.UserId,
                                            user = new User
                                            {
                                                full_name = u.FullName
                                            }
                                        },
                                        status = (int)p.Status,
                                    };
                        item = query.ToHashSet();
                    }

                }
                return item;
            }
            catch (Exception ex)
            {

            }
            return new HashSet<PaymentHistory>();
        }
        public HashSet<PaymentHistory> Paging(int month = 0, int page = 1, int pageSize = 10, int client = 0)
        {
            try
            {
                var item = new HashSet<PaymentHistory>();
                if (client == 0)
                {
                    var query = from p in _context.TblPaymentHistories
                                join c in _context.TblContracts on p.ContractId equals c.Id
                                join u in _context.TblUsers on c.UserId equals u.Id
                                select new PaymentHistory
                                {
                                    id = p.Id,
                                    amount = (int)p.Amount,
                                    contract_id = (int)p.ContractId,
                                    payment_day = p.PaymentDay,
                                    Contract = new ContractView
                                    {
                                        id = c.Id,
                                        user_id = (int)c.UserId,
                                        user = new User
                                        {
                                            full_name = u.FullName
                                        }
                                    },
                                    status = (int)p.Status,
                                };
                    if (month == 0)
                    {
                        item = query.Skip((page - 1) * pageSize).Take(pageSize).ToHashSet();
                    }
                    else if (month > 0 && month < 12)
                    {
                        item = query.Where(d => d.payment_day.Month == month).Skip((page - 1) * pageSize).Take(pageSize).ToHashSet();
                    }
                }
                else
                {

                    if (month == 0)
                    {
                        var query = from p in _context.TblPaymentHistories
                                    join c in _context.TblContracts on p.ContractId equals c.Id
                                    join u in _context.TblUsers on c.UserId equals u.Id
                                    where u.Id == client
                                    select new PaymentHistory
                                    {
                                        id = p.Id,
                                        amount = (int)p.Amount,
                                        contract_id = (int)p.ContractId,
                                        payment_day = p.PaymentDay,
                                        Contract = new ContractView
                                        {
                                            id = c.Id,
                                            user_id = (int)c.UserId,
                                            user = new User
                                            {
                                                full_name = u.FullName
                                            }
                                        },
                                        status = (int)p.Status,
                                    };
                        item = query.Skip((page - 1) * pageSize).Take(pageSize).ToHashSet();
                    }
                    else if (month > 0 && month < 12)
                    {
                        var query = from p in _context.TblPaymentHistories
                                    join c in _context.TblContracts on p.ContractId equals c.Id
                                    join u in _context.TblUsers on c.UserId equals u.Id
                                    where u.Id == client && p.PaymentDay.Month == month
                                    select new PaymentHistory
                                    {
                                        id = p.Id,
                                        amount = (int)p.Amount,
                                        contract_id = (int)p.ContractId,
                                        payment_day = p.PaymentDay,
                                        Contract = new ContractView
                                        {
                                            id = c.Id,
                                            user_id = (int)c.UserId,
                                            user = new User
                                            {
                                                full_name = u.FullName
                                            }
                                        },
                                        status = (int)p.Status,
                                    };
                        item = query.Skip((page - 1) * pageSize).Take(pageSize).ToHashSet();
                    }
                }
                return item;
            }
            catch (Exception ex)
            {

            }
            return new HashSet<PaymentHistory>();
        }
        public bool Update(PaymentHistory entity)
        {
            throw new NotImplementedException();
        }
        public HashSet<PaymentHistory> FindByContractId(int contractId)
        {

            try
            {
                var payments = _context.TblPaymentHistories
                    .Where(x => x.ContractId == contractId)
                    .Select(c => new PaymentHistory
                    {
                        id = c.Id,
                        contract_id = (int)c.ContractId,
                        amount = (long)c.Amount,
                        payment_day = (DateTime)c.PaymentDay,

                        status = (int)c.Status,
                    }).ToHashSet();
                return payments;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return new HashSet<PaymentHistory>();

            }
        }
        public HashSet<PaymentHistory> GetByContractId(int contractId)
        {
            try
            {
                var payments = _context.TblPaymentHistories
                    .Where(x => x.ContractId == contractId)
                    .Select(c => new PaymentHistory
                    {
                        id = c.Id,
                        contract_id = (int)c.ContractId,
                        amount = (long)c.Amount,
                        payment_day = (DateTime)c.PaymentDay,
                        status = (int)c.Status,
                    }).ToHashSet();
                return payments;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return new HashSet<PaymentHistory>();
            }
        }

        public HashSet<PaymentHistory> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
