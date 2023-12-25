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

        public Like Getlikeby_User(int id_user)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_like_user", "@ID_User", id_user);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Like>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Like Getlikeby_Posts(int id_post)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_like_posts", "@ID_Posts", id_post);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Like>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create_Like(Like model)
        {
            string msgError = "";
            try
            {
                var result = _idbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_like_create",
                "@ID_User", model.ID_User,
                "@ID_Posts", model.ID_Post);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Delete_Like(Like model)
        {
            string msgError = "";
            try
            {
                var result = _idbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_like_delete",
                "@ID_User", model.ID_User,
                "@ID_Posts", model.ID_Post);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
