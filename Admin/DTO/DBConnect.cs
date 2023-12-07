using Microsoft.Data.SqlClient;

namespace DTO
{
    public class DBConnect
    {
        protected SqlConnection cns = new SqlConnection("Server=LAPTOP-B8V4R50K\\SQLEXPRESS;Database=DienDan_Vinfast;Trusted_Connection=True;");

    }
}
