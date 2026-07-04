using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_De_Venta
{
    internal class Conexion
    {
        private readonly string cadena;

        public Conexion()
        {
            cadena = "Server=localhost; Database=puntobd; Uid=root; Pwd=; Port=3307";
        }

        public MySqlConnection getConection()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(cadena);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de coneccion : " + ex.Message);
                return null;
            }
        }
    }
}
