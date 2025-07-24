using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Models.Repository
{
    public class NotificationRepository : IRepository<Notification>
    {
        private readonly InsuranceContext _context;
        //create singleton instance of InsuranceTypeRepository
        private static NotificationRepository? _instance;
        public static NotificationRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationRepository();
                }
                return _instance;
            }
        }

        private NotificationRepository()
        {
            _context = new InsuranceContext();
        }

        public bool Create(Notification entity)
        {

            try
            {
                var notification = new TblNotification
                {
                    Detail = entity.detail,
                    NotificationTypeId = entity.notification_type_id,

                    From = entity.from,
                    To = entity.to,

                    IsRead = entity.is_read
                };
                _context.TblNotifications.Add(notification);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return false;
            }


        }

        public bool Update(Notification entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public Notification FindById(int id)
        {

            var notification = _context.TblNotifications.Find(id);
            if (notification == null)
            {
                return null;
            }
            return new Notification
            {
                id = notification.Id,
                detail = notification.Detail ?? string.Empty,
                notification_type_id = notification.NotificationTypeId ?? 0,
                from = notification.From ?? 0,
                to = notification.To,
                is_read = notification.IsRead ?? 0
            };
        }

        public HashSet<Notification> FindByKeywork(string keywork)
        {
            throw new NotImplementedException();
        }
        public HashSet<Notification> GetUnreadNotificationsByUserId(int userId)
        {
            var notifications = _context.TblNotifications
                .Where(n => n.To == userId)
                .Select(n => new Notification
                {
                    id = n.Id,
                    detail = n.Detail ?? string.Empty,
                    notification_type_id = n.NotificationTypeId ?? 0,
                    user_id = n.To,
                    is_read = n.IsRead ?? 0
                })
                .ToHashSet();
            return notifications;
        }
        public HashSet<Notification> GetAllNotificationsByUserId(int userId)
        {
            var notifications = _context.TblNotifications
                .Where(n => n.To == userId && n.IsRead == 0)
                .Select(n => new Notification
                {
                    id = n.Id,
                    detail = n.Detail ?? string.Empty,
                    notification_type_id = n.NotificationTypeId ?? 0,
                    user_id = n.To,
                    is_read = n.IsRead ?? 0
                })
                .ToHashSet();
            return notifications;
        }
    }
}
