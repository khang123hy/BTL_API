using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_API_ThongKe : ControllerBase
    {


        private ITF_BLL_ThongKe _BLL_ThongKe;
        public Admin_API_ThongKe(ITF_BLL_ThongKe bLL_ThongKe)
        {
            _BLL_ThongKe = bLL_ThongKe;
        }
        [Route("ThongKe_NguoiDungMoi")]
        [HttpGet]
        public ThongKe_User ThongKe_NguoiDungMoi(string hienthitheo, int nam)
        {
            return _BLL_ThongKe.ThongKe_NguoiDungMoi(hienthitheo, nam);
        }

        [Route("ThongKe_TongNguoiDung")]
        [HttpGet]
        public ThongKe_User ThongKe_TongNguoiDung()
        {
            return _BLL_ThongKe.ThongKe_TongNguoiDung();
        }
        [Route("ThongKe_BaiVietMoi")]
        [HttpGet]
        public ThongKe_Post ThongKe_BaiVietMoi(string hienthitheo, int nam)
        {
            return _BLL_ThongKe.ThongKe_BaiVietMoi(hienthitheo, nam);
        }
        [Route("ThongKe_BinhLuanMoi")]
        [HttpGet]
        public ThongKe_Comment ThongKe_BinhLuanMoi(string hienthitheo, int nam)
        {
            return _BLL_ThongKe.ThongKe_BinhLuanMoi(hienthitheo, nam);
        }


        [Route("GetTopLikedPosts")]
        [HttpPost]
        public IActionResult GetTopLikedPosts([FromBody] DienThongKe formData)
        {
            try
            {
                var data = _BLL_ThongKe.GetTopLikedPosts(formData.TimeFrame, formData.Year, formData.Month);

                return Ok(
                    new
                    {
                        Data = data
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
