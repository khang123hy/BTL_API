
using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_Users : ITF_DAL_Users
    {
        private IDatabaseHelper _dbhelper;
        public DAL_Users(IDatabaseHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        #region User
        public User GetUser_byID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_getid", "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<User>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Create_User(User model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_create",
                      "@FullName", model.FullName,
                      "@Address", model.Address,
                      "@Sex", model.Sex,
                      "@DateOfBirth", model.DateOfBirth,
                      "@PhoneNumber", model.PhoneNumber,
                      "@AccountName", model.AccountName,
                      "@Password", model.Password,
                      "@Email", model.Email,
                      "@Role", model.Role,
                      "@Avatar", model.Avatar
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

        public bool Update_User(User2 model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_update",
                "@ID_User", model.ID_User,
                "@FullName", model.FullName,
                "@Address", model.Address,
                "@Sex", model.Sex,
                "@DateOfBirth", model.DateOfBirth,
                "@PhoneNumber", model.PhoneNumber,
                 "@Avatar", model.Avatar

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

        public bool Update_User_ALL(User model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_update_all",
                "@ID_User", model.ID_User,
                "@Password", model.Password,
                "@Email", model.Email,
                "@Role", model.Role,
                "@FullName", model.FullName,
                "@Address", model.Address,
                "@Sex", model.Sex,
                "@DateOfBirth", model.DateOfBirth,
                "@PhoneNumber", model.PhoneNumber,
                 "@Avatar", model.Avatar

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

        public User Delete_User(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_delete", "@ID_User", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<User>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool Deletes_User(List_User model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_user_deletes",
                "@list_json_ID_User", model.list_json_ID_User != null ? MessageConvert.SerializeObject(model.list_json_ID_User) : null);
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

        public List<User> Search_User(int pageIndex, int pageSize, out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<User>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



    }
}
