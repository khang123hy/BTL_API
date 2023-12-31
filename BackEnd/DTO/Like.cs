namespace DTO
{
    public partial class Like
    {
        public int? ID_Likes { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }

    }

    public partial class Like2
    {
        public int? ID_Likes { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }

    }

    public partial class LikeAndNotification
    {

        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }
        public int? ID_User_Nhan { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }


    }
}
