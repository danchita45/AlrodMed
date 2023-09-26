using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;

namespace ProyectoAlrod.Models
{
    public class PacientesConnection
    {
        public static System.Data.DataTable ViewPacients()
        {

            try
            {
                Connections con = new Connections();


                string sql = "EXEC dbo.spSelectPacientsAll";
                SqlCommand command = new SqlCommand(sql, con.conectar());
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                con.desconectar();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
