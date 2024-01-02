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



        #region Account
        public Check check_AccountName(string AccountName, string Email)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_check_AccountName", "@AccountName", AccountName, "@Email", Email);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Check>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create_Account(Account model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_createLogin",
                "@AccountName", model.AccountName,
                "@Password", model.Password,
                "@Email", model.Email,
                "@Avatar", model.Avatar,
                "@FullName", model.FullName,
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



    }
}
#endregion