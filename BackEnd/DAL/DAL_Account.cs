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
        public bool Create_Account(Account model)
        {
            string msgError = "";
            try
            {
                var result = _dbhelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_createLogin",
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



    }
}
#endregion