using Microsoft.Data.SqlClient;

namespace ProyectoModeradores.Controllers
{
    public class Connections
    {
        SqlConnection con;

        public Connections()
        {
            con = new SqlConnection("Server= DANCHITA45\\SQLSERVER2014;Database=MedAlrod;integrated security=True;Encrypt=False ");
        }

        public SqlConnection conectar()
        {
            try
            {
                con.Open();
                return con;

            }catch (Exception ex)
            {
                return  null;
            }

        }

        public SqlConnection desconectar()
        {
            try
            {
                con.Close();
                return con;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
