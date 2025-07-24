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
                from = notification.From ,
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
            var query = from n in _context.TblNotifications
                        join fromUser in _context.TblUsers on n.From equals fromUser.Id into fromUserGroup
                        from fromUser in fromUserGroup.DefaultIfEmpty()

                        join toUser in _context.TblUsers on n.To equals toUser.Id into toUserGroup
                        from toUser in toUserGroup.DefaultIfEmpty()

                        where n.To == userId
                        select new Notification
                        {
                            id = n.Id,
                            detail = n.Detail ?? string.Empty,
                            @from = n.From,
                            notification_type_id = n.NotificationTypeId ?? 0,
                            to = n.To,
                            employee = fromUser != null ? new User
                            {
                                id = fromUser.Id,
                                full_name = fromUser.FullName ?? string.Empty,
                                email = fromUser.Email ?? string.Empty,
                                phone = fromUser.Phone ?? string.Empty,
                                address = fromUser.Address ?? string.Empty,
                                avatar = fromUser.Avatar ?? string.Empty,
                            } : null,
                            is_read = n.IsRead ?? 0,
                            user_id = toUser != null ? toUser.Id : 0
                        };

            var notifications = query.ToList().ToHashSet();

            return notifications;
        }
    }
}
