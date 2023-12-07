﻿
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



    }
}
