using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinContador.Data;

namespace WinContador
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += FrmLogin_KeyDown;
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEntrar.PerformClick();

            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            ConfigRepository configRepository = new ConfigRepository();
            configRepository.CrearBaseSiNoExiste();

            string user = configRepository.Obtener("USER");
            string password = configRepository.Obtener("PASSWORD");

            if (user == txtUsuario.Text.Trim() && password == txtContrasena.Text.Trim())
            {
                FrmPrincipal principal = new FrmPrincipal();
                principal.Show();
                this.Hide();
            }
            else
            {
                limpiar();
                MessageBox.Show("Usuario o contraseña incorrectos","Validación",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }

        private void limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            txtUsuario.Focus();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}



//this.KeyPreview = true;
//this.KeyDown += FrmPrincipal_KeyDown;

//LimpiarForm();
//        }

//        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
//{
//    // + => siempre suma
//    if (e.KeyCode == Keys.Add)
//    {
//        try
//        {
//            rbSuma.Checked = true;
//            // Ejecutar la misma acción que el botón procesar
//            btnProcesa.PerformClick();
//        }
//        catch
//        {
//            // ignorar si no existen los controles en tiempo de diseño
//        }
//        e.Handled = true;
//    }

//    // - => siempre resta
//    if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
//    {
//        try
//        {
//            rbResta.Checked = true;
//            btnProcesa.PerformClick();
//        }
//        catch
//        {
//            // ignorar si no existen los controles en tiempo de diseño
//        }
//        e.Handled = true;
//    }

//    // con enter procesar
//    if (e.KeyCode == Keys.Enter)
//    {
//        btnProcesa.PerformClick();
//    }
//}