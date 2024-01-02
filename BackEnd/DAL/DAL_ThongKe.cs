using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_ThongKe : ITF_DAL_ThongKe
    {

        private IDatabaseHelper _dbhelper;
        public DAL_ThongKe(IDatabaseHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }
        public ThongKe_User ThongKe_NguoiDungMoi(string hienthitheo, int nam)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_ThongKe_NguoiDungMoi", "@TimeFrame", hienthitheo, "@Year", nam);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ThongKe_User>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ThongKe_User ThongKe_TongNguoiDung()
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_ThongKe_TongNguoiDung");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ThongKe_User>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ThongKe_Post ThongKe_BaiVietMoi(string hienthitheo, int nam)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_BaiViet_BaiVietMoi", "@TimeFrame", hienthitheo, "@Year", nam);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ThongKe_Post>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ThongKe_Comment ThongKe_BinhLuanMoi(string hienthitheo, int nam)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Comment_binhluanmoi", "@TimeFrame", hienthitheo, "@Year", nam);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ThongKe_Comment>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ThongKe_Like> GetTopLikedPosts(string timeFrame, int year = 0, int month = 0)
        {
            string msgError = "";

            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_TopLikedPosts",
                    "@TimeFrame", timeFrame,
                    "@Year", year,
                    "@Month", month);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return dt.ConvertTo<ThongKe_Like>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
