
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


        public bool Create_User(User2 model)
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

        public bool Update_User(User model)
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
                "@PhoneNumber", model.PhoneNumber
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
        #endregion


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

        #endregion

    }
}
