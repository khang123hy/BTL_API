using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_Like : ITF_DAL_Like
    {
        private IDatabaseHelper _idbhelper;
        public DAL_Like(IDatabaseHelper idbhelper)
        {
            _idbhelper = idbhelper;
        }

        public Like GetlikebyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_like_id", "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Like>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
