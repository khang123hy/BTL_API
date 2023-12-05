
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



        public User GetUser_byID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbhelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_get_by_id", "@ID_User", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<User>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
