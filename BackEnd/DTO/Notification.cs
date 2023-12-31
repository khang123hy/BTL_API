namespace DTO
{
    public partial class Notification
    {
        public int ID_Notification { get; set; }
        public int? ID_User_Nhan { get; set; }
        public int? ID_User_Tao { get; set; }
        public int? ID_Like_or_Comment { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string Link { get; set; }
        public DateTime? NotificationDate { get; set; }
    }
    public partial class Notification2
    {
        public int ID_Notification { get; set; }
        public int? ID_User_Nhan { get; set; }
        public int? ID_User_Tao { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public string Link { get; set; }
        public string FullName { get; set; }
        public DateTime? NotificationDate { get; set; }
    }

    public partial class LIST_Notification
    {
        public List<CT_Notification> list_notification { get; set; }
    }

    public partial class CT_Notification
    {
        public int ID_Notification { get; set; }

    }
}
