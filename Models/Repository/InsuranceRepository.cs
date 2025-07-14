using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class InsuranceRepository : IRepository<InsuranceView>
    {
        private static InsuranceRepository _instance = null;
        private InsuranceRepository()
        {
            _context = new InsuranceContext();
        }
        public static InsuranceRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InsuranceRepository();
                }
                return _instance;
            }
        }
        public InsuranceContext _context;
        public bool Create(InsuranceView entity)
        {
            try
            {
                if (entity != null)
                {
                    var Insurance = new Insurance
                    {
                        Id = entity.id,
                        Name = entity.name,
                        Description = entity.description,
                        InsuranceTypeId = entity.insurance_type_id,
                        Value = entity.value,
                        YearMax = entity.year_max,

                        ExImage = entity.ex_image,
                    };
                    var q = _context.Add(Insurance);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }



        public InsuranceView FindById(int id)
        {

            try
            {
                var item = _context.Insurances.Where(d => d.Id == id).Select(d => new InsuranceView
                {
                    id = d.Id,
                    name = d.Name,
                    description = d.Description,
                    year_max = (int)d.YearMax,
                    value = d.Value ?? 0,



                    insurance_type_id = (int)d.InsuranceTypeId,

                    ex_image = d.ExImage
                }).FirstOrDefault();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public HashSet<InsuranceView> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }

        public HashSet<InsuranceView> GetAll()
        {
            try
            {
                var item = _context.Insurances.Select(d => new InsuranceView
                {
                    id = d.Id,
                    name = d.Name,
                    description = d.Description,
                    insurance_type_id = (int)d.InsuranceTypeId,
                    value = d.Value ?? 0,
                    year_max = d.YearMax??0,

                    ex_image = d.ExImage
                }).ToHashSet();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {
            }
            return new HashSet<InsuranceView>();
        }

        public bool Update(InsuranceView entity)
        {
            try
            {
                if (entity != null)
                {
                    var q = _context.Insurances.Where(d => d.Id == entity.id).FirstOrDefault();
                    q.InsuranceTypeId = entity.insurance_type_id;
                    q.YearMax = entity.year_max;
                    q.ExImage = entity.ex_image;
                    q.Value = entity.value;
                    q.Name = entity.name;
                    q.Description = entity.description;
                    if (entity.ex_image != null && entity.ex_image != string.Empty)
                    {
                        q.ExImage = entity.ex_image;
                    }
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public HashSet<InsuranceView> Paging(int  page=1, int pageSize=10)
        {
            try
            {
                var item = _context.Insurances.Skip((page-1)*pageSize).Take(pageSize).Select(d=> new InsuranceView
                {
                    id = d.Id,
                    name = d.Name,
                    description = d.Description,
                    insurance_type_id = (int)d.InsuranceTypeId,
                    value = d.Value ?? 0,
                    year_max = d.YearMax ?? 0,

                    ex_image = d.ExImage
                }).ToHashSet();
                return item;
            }
            catch (Exception ex)
            {
            }
            return new HashSet<InsuranceView>();
        }
        public bool CheckInsurance(int id)
        {
            try
            {
                var q = _context.TblContracts.Any(d => d.InsuranceId == id);
                return q;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public bool Delete(int id = 0)
        {
            try
            {
                if (!CheckInsurance(id))
                {
                    if (id > 0)
                    {
                        var q = _context.Insurances.Any(d => d.Id == id);
                        if (q)
                        {
                            var item = _context.Insurances.Where((d) => d.Id == id).FirstOrDefault();
                            _context.Insurances.Remove(item);
                            _context.SaveChanges();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public HashSet<InsuranceView> FindByInsuranceTypeId(int id)
        {

            try
            {
                var item = _context.Insurances.Where(d => d.InsuranceTypeId == id).Select(d => new InsuranceView
                {
                    id = d.Id,
                    name = d.Name,
                    description = d.Description,
                    year_max =d.YearMax?? 0,
                    value = d.Value ?? 0,
                    insurance_type_id = (int)d.InsuranceTypeId,
                    ex_image = d.ExImage
                }).ToHashSet();
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {
            }
            return new HashSet<InsuranceView>();
        }
    }
}
