using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;
using static DTO.Post;

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
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_posts_create",
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

        public Post Delete_Post(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_posts_delete", "@ID_Post", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Post>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Deletes_Post(LIST_Post model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_post_deletes",
                "@list_post", model.list_post != null ? MessageConvert.SerializeObject(model.list_post) : null);
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

        public List<Post> Search_Posts(int pageIndex, int pageSize, out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Posts_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Post>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
