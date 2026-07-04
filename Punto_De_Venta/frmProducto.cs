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
    public partial class frmProducto : Form
    {
        Conexion conexion = new Conexion();
        int idSeleccionado = 0;
        public frmProducto()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos(string filtro = "")
        {
            try
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    string query = "SELECT * FROM productos WHERE descripcion LIKE @filtro OR codigo LIKE @filtro";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvProductos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CargarDatos(txtBusqueda.Text);
        }
    }
}
