using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_Post : ITF_DAL_Post
    {
        private IDatabaseHelper _dbhelper;
        public DAL_Post(IDatabaseHelper idbhelper)
        {
            _dbhelper = idbhelper;
        }

        public Post getpost(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_post_byid", "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Post>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create_Post(Post model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "Post_create",
                "@ID_User", model.ID_User,
                "@ID_Topic", model.ID_Topic,
                "@Title", model.Title,
                "@Content", model.Content,
                "@Image", model.Image);
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

        public bool Update_Post(Post model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "Post_update",
                "@ID_Post", model.ID_Post,
                "@ID_User", model.ID_User,
                "@ID_Topic", model.ID_Topic,
                "@Title", model.Title,
                "@Content", model.Content,
                "@Image", model.Image);
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
