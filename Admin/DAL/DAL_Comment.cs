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

        public bool Create_Comment(Comment model)
        {
            string msgError = "";
            try
            {
                var result = _idbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "Comment_Create",
                "@ID_Post", model.ID_Post,
                "@Content", model.Content,
                "@ID_User", model.ID_User);
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

        public bool Update_Comment(Comment model)
        {
            string msgError = "";
            try
            {
                var result = _idbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "Comment_Update",
                "@ID_Comment", model.ID_Comment,
                "@ID_Post", model.ID_Post,
                "@Content", model.Content,
                "@ID_User", model.ID_User);
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

        public Comment Delete_Comment(int id)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "Comment_Delete", "@ID_Comment", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Comment>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Deletes_Notification(LIST_Comment model)
        {
            string msgError = "";
            try
            {
                var result = _idbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_comment_deletes",
                "@list_comment", model.list_comment != null ? MessageConvert.SerializeObject(model.list_comment) : null);
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


        public List<Comment> Search_Comment(int pageIndex, int pageSize, out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Comment_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Comment>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
