namespace DTO
{
    public partial class Notification
    {


        public int ID_Notification { get; set; }
        public int? ID_User { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? NotificationDate { get; set; }

        public Notification()
        {

        }
        public Notification(int idNotification, int? idUser, string content, DateTime? notificationDate)
        {
            ID_Notification = idNotification;
            ID_User = idUser;
            Content = content;
            NotificationDate = notificationDate;
        }

    }
}
