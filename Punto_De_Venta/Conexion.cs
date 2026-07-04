using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_De_Venta
{

        public class Conexion
        {
            private string cadena = "Server=localhost; Database=Puntodb; Uid=root; Pwd=; Port=3307";

            public MySqlConnection GetConnection()
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(cadena);
                    conn.Open();
                    return conn;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al conectar con la base de datos: " + ex.Message);
                }
            }
        }   
}