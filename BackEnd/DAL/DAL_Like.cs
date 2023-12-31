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

        public Like Getlikeby_User(int id_user, int id_post)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_like_user", "@ID_User", id_user, "@ID_Post", id_post);
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
        public bool Create_Like_Notification(LikeAndNotification model)
        {
            string msgError = "";
            try
            {
                var result = _idbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "Like_Create_thongbao",
                "@ID_User", model.ID_User,
                "@ID_Post", model.ID_Post,
                "@ID_User_Nhan", model.ID_User_Nhan,
                "@Link", model.Link,
                "@Title", model.Title
                );
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

        public Like Delete_Like(int id)
        {
            string msgError = "";
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_like_Delete_Notification", "@ID_Likes", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Like>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Like2> Search_Like_Posts(out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _idbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_search_like_posts",
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Like2>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
