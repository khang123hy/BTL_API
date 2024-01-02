using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_ThongKe
    {
        ThongKe_User ThongKe_NguoiDungMoi(string hienthitheo, int nam);
        ThongKe_User ThongKe_TongNguoiDung();
        ThongKe_Post ThongKe_BaiVietMoi(string hienthitheo, int nam);
        public ThongKe_Comment ThongKe_BinhLuanMoi(string hienthitheo, int nam);
        List<ThongKe_Like> GetTopLikedPosts(string timeFrame, int year = 0, int month = 0);

    }
}
