using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_De_Venta
{
    public partial class FrmLogin : Form
    {
        Conexion conexion = new Conexion();
        public FrmLogin()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Campos vacíos");
                return;
            }

            try
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    string query = "SELECT nombre_completo FROM usuarios WHERE username=@user AND password=@pass";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", txtUser.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        MessageBox.Show("Bienvenido " + resultado.ToString());

                        frmProducto frm = new frmProducto();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
