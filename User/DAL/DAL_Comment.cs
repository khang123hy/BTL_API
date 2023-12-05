using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_Comment : ITF_DAL_Comment
    {
        private IDatabaseHelper _idbhelper;
        public DAL_Comment(IDatabaseHelper idbhelper)
        {
            _idbhelper = idbhelper;
        }

        public Comment getComment_byid(int id)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_comment_id", "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Comment>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
