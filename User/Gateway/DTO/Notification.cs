namespace DTO
{
    public partial class Notification
    {


        public int IdNotification { get; set; }
        public int? IdUser { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? NotificationDate { get; set; }

        public Notification()
        {

        }
        public Notification(int idNotification, int? idUser, string content, DateTime? notificationDate)
        {
            IdNotification = idNotification;
            IdUser = idUser;
            Content = content;
            NotificationDate = notificationDate;
        }

    }
}
