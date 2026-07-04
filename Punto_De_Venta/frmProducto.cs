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
            {
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) ||
                    !int.TryParse(txtStock.Text, out int stock))
                {
                    MessageBox.Show("Datos inválidos");
                    return;
                }

                try
                {
                    using (MySqlConnection conn = conexion.GetConnection())
                    {
                        string query = @"INSERT INTO productos    (codigo, descripcion, precio, stock, categoria) VALUES (@cod, @nom, @precio, @stock, @cat)";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@cod", txtCodigo.Text);
                        cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@cat", cmbCategorias.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Producto agregado");
                        CargarDatos();
                        Limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
        private void Limpiar()
        {
            lblId.Text = "0";
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            cmbCategorias.SelectedIndex = -1;
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                lblId.Text = fila.Cells["producto_id"].Value.ToString();
                txtCodigo.Text = fila.Cells["codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["descripcion"].Value.ToString();
                txtPrecio.Text = fila.Cells["precio"].Value.ToString();
                txtStock.Text = fila.Cells["stock"].Value.ToString();
                cmbCategorias.Text = fila.Cells["categoria"].Value.ToString();
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lblId.Text == "0")
            {
                MessageBox.Show("Selecciona un producto");
                return;
            }

            try
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    string query = @"UPDATE productos SET codigo=@cod, descripcion=@nom, precio=@precio, stock=@stock, categoria=@cat WHERE producto_id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@cod", txtCodigo.Text);
                    cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@precio", txtPrecio.Text);
                    cmd.Parameters.AddWithValue("@stock", txtStock.Text);
                    cmd.Parameters.AddWithValue("@cat", cmbCategorias.Text);
                    cmd.Parameters.AddWithValue("@id", lblId.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Producto actualizado");
                    CargarDatos();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
