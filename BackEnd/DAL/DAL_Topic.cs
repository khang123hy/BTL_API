using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_Topic : ITF_DAL_Topic
    {
        private IDatabaseHelper _dbhelper;
        public DAL_Topic(IDatabaseHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public Topic GetTopicbyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "get_topic", "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Topic>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Topic Delete(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_topic_delete", "@ID_Topic", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Topic>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(Topic model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_topic_create",
                "@Title", model.Title,
                "@Description", model.Description,
                "@Image", model.Image
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

        public bool Update(Topic model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_topic_update",
                "@ID_Topic", model.ID_Topic,
                "@Title", model.Title,
                "@Description", model.Description,
                "@Image", model.Image


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

        public bool Deletes_Topic(LIST_Topic model)
        {
            // Chuỗi để lưu trữ thông báo lỗi
            string msgError = "";

            try
            {
                // Gọi stored procedure từ đối tượng _dbhelper
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(
                    out msgError,
                    "sp_topic_deletes", // Tên stored procedure
                    "@list_topic",
                    model.list_topic != null ? MessageConvert.SerializeObject(model.list_topic) : null
                );

                // Kiểm tra kết quả stored procedure và thông báo lỗi
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    // Nếu có lỗi, ném một ngoại lệ với thông báo lỗi
                    throw new Exception(Convert.ToString(result) + msgError);
                }

                // Nếu không có lỗi, trả về true để thể hiện thành công
                return true;
            }
            catch (Exception ex)
            {
                // Nếu có ngoại lệ, ném lại để xử lý ở cấp độ cao hơn
                throw ex;
            }
        }


        public List<Topic> Search_Topic(int pageIndex, int pageSize, out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Topic_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Topic>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
