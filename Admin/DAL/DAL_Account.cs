using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using DTO;

namespace DAL
{
    public class DAL_Account : ITF_DAL_Account
    {
        private IDatabaseHelper _dbhelper;
        public DAL_Account(IDatabaseHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        //public LoginResult Login(string taikhoan, string matkhau)
        //{
        //    string msgError = "";
        //    try
        //    {
        //        var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_login",
        //             "@accountname", taikhoan,
        //             "@password", matkhau
        //             );
        //        if (!string.IsNullOrEmpty(msgError))
        //            throw new Exception(msgError);
        //        return dt.ConvertTo<LoginResult>().FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        #region Account
        public bool Update_Account(Account model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_account_update",
                "@ID_User", model.ID_User,
                "@AccountName", model.AccountName,
                "@Password", model.Password,
                "@Role", model.Role
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

        public Account Delete_Account(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_account_delete", "@ID_User", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Account>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool Deletes_Account(List_User model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_account_deletes",
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

        public List<Account> Search_Account(int pageIndex, int pageSize, out long total, string Keywords)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Account_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Keywords", Keywords);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<Account>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
