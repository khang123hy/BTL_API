namespace DTO
{
    public partial class Notification
    {


        public int ID_Notification { get; set; }
        public int? ID_User { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? NotificationDate { get; set; }

        public partial class LIST_Notification
        {
            public List<CT_Notification> list_notification { get; set; }
        }

        public partial class CT_Notification
        {
            public int ID_Notification { get; set; }

        }

    }
}
