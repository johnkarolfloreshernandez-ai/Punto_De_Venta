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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void AbrirFormulario(Type tipoFormulario)
        {
            foreach (Form hijo in this.MdiChildren)
            {
                if (hijo.GetType() == tipoFormulario)
                {
                    hijo.BringToFront();
                    return;
                }
            }

            Form form = (Form)Activator.CreateInstance(tipoFormulario);
            form.MdiParent = this;
            form.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(typeof(frmProducto));
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
