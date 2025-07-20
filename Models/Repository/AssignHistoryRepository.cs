using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class AssignHistoryRepository : IRepository<AssignHistoryView>
    {

        private InsuranceContext _context;

        private static AssignHistoryRepository _instance;
        public static AssignHistoryRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssignHistoryRepository();
                }
                return _instance;
            }
        }
        private AssignHistoryRepository()
        {
            _context = new InsuranceContext();
        }
        public bool Create(AssignHistoryView entity)
        {


            var result = _context.TblAssignHistories.Add(

                new TblAssignHistory
                {
                    EmployeeId = entity.employee_id,
                    ContractId = entity.contract_id,
                    StartDate = entity.start_date,
                    EndDate = entity.end_date
                }
                );
            _context.SaveChanges();
            return result != null;






        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AssignHistoryView FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<AssignHistoryView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<AssignHistoryView> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(AssignHistoryView entity)
        {
           
            var history = _context.TblAssignHistories.FirstOrDefault(h => h.Id == entity.id);
            if (history != null)
            {
                history.EmployeeId = entity.employee_id;
                history.ContractId = entity.contract_id;
                history.StartDate = entity.start_date;
                history.EndDate = entity.end_date;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateEmployeeId(int id, int employeeId)
        {
            // Implementation to update employee_id for the given id
            throw new NotImplementedException();
        }
        public AssignHistoryView FindUnCompletedHistory(int contractId, int employeeId)
        {

            var history = _context.TblAssignHistories
                .Where(h => h.ContractId == contractId && h.EmployeeId == employeeId && h.EndDate == null)
                .Select(h => new AssignHistoryView
                {
                    id = h.Id,
                    employee_id = h.EmployeeId,
                    contract_id = h.ContractId,
                    start_date = (DateTime)h.StartDate,
                    end_date = h.EndDate
                })
                .FirstOrDefault();
            return history;
        }
    }
}
