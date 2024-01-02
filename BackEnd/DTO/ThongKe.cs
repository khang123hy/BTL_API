namespace DTO
{
    public partial class ThongKe_User
    {
        public int NewUserCount { get; set; }
        public int TongNguoiDung { get; set; }

    }
    public partial class ThongKe_Post
    {
        public int NewPostCount { get; set; }
    }
    public partial class ThongKe_Comment
    {
        public int NewCommentCount { get; set; }
    }

    public partial class ThongKe_Like
    {
        public int ID_Post { get; set; }
        public string Title { get; set; }
        public int LikeCount { get; set; }
    }


    public class DienThongKe
    {
        public string TimeFrame { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

}
