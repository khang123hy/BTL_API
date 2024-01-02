using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_ThongKe : ITF_BLL_ThongKe
    {

        public ITF_DAL_ThongKe _DAL_ThongKe;
        public BLL_ThongKe(ITF_DAL_ThongKe dAL_ThongKe)
        {
            _DAL_ThongKe = dAL_ThongKe;
        }
        public ThongKe_User ThongKe_NguoiDungMoi(string hienthitheo, int nam)
        {
            return _DAL_ThongKe.ThongKe_NguoiDungMoi(hienthitheo, nam);
        }
        public ThongKe_User ThongKe_TongNguoiDung()
        {
            return _DAL_ThongKe.ThongKe_TongNguoiDung();
        }
        public ThongKe_Post ThongKe_BaiVietMoi(string hienthitheo, int nam)
        {
            return _DAL_ThongKe.ThongKe_BaiVietMoi(hienthitheo, nam);
        }
        public ThongKe_Comment ThongKe_BinhLuanMoi(string hienthitheo, int nam)
        {
            return _DAL_ThongKe.ThongKe_BinhLuanMoi(hienthitheo, nam);
        }
        public List<ThongKe_Like> GetTopLikedPosts(string timeFrame, int year = 0, int month = 0)
        {
            return _DAL_ThongKe.GetTopLikedPosts(timeFrame, year, month);
        }
    }
}
