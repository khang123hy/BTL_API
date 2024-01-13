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


        public Post3 getpost_by_id_User(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_post_byid_User", "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Post3>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<list_json_post> Search_PostDetails_User(out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_search_PostDetails_by_posts",
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<list_json_post>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool Create_post_list(Post_list model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_Posts_create_list",
                "@ID_User", model.ID_User,
                "@ID_Topic", model.ID_Topic,
                "@Title", model.Title,
                "@Synopsis", model.Synopsis,
                "@list_json_PostDetails", model.list_json_posts != null ? MessageConvert.SerializeObject(model.list_json_posts) : null);
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

        public bool Update_post_list(Post_list model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_Posts_update_List",
                "@ID_Post", model.ID_Post,
                "@ID_User", model.ID_User,
                "@ID_Topic", model.ID_Topic,
                "@Title", model.Title,
                "@Synopsis", model.Synopsis,
                "@list_json_PostDetails", model.list_json_posts != null ? MessageConvert.SerializeObject(model.list_json_posts) : null);
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
        public List<Post_list> Search_Posts_Admin(int pageIndex, int pageSize, out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Posts_search_list",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Post_list>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Post2> Search_Posts_User_Desc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Posts_search_User_Desc",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords,
                    "@OrderBy", OrderBy);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Post2>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Tạo phương thức trả về List<Post2>
        public List<Post2> Search_Posts_User_Asc(int pageIndex, int pageSize, out long total /*được gán giá trị tổng số bản ghi*/, string Keywords, string OrderBy)
        {
            //Lưu trữ thông báo lỗi từ Proc
            string msgError = "";
            total = 0;
            try
            {
                //trả kết quả proc ra dạng datatable 
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Posts_search_User_Asc",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords,
                    "@OrderBy", OrderBy);
                //kiểm tra thông báo lỗi có từ proc ko nếu từ proc thì vứt vào msgError
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                //Nếu DataTable có ít nhất một dòng, lấy giá trị của cột "RecordCount" từ dòng đầu tiên và gán cho biến total
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];

                //Chuyển đổi kết quả datatable sang các đối tượng post2 và trả về kết quả list<post2>
                return dt.ConvertTo<Post2>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Post2> Search_Posts_by_Topic_User_Desc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy, string ID_Topic)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Posts_search_by_topic_User_desc",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords,
                    "@ID_Topic", ID_Topic,
                    "@OrderBy", OrderBy
                    );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Post2>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Post2> Search_Posts_by_Topic_User_Asc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy, string ID_Topic)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Posts_search_by_topic_User_ASC",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords,
                    "@ID_Topic", ID_Topic,
                    "@OrderBy", OrderBy
                    );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Post2>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
