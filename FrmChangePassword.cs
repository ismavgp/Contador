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
    public partial class FrmChangePassword : Form
    {
        private readonly ConfigRepository _configRepository;

        public FrmChangePassword()
        {
            InitializeComponent();
            _configRepository = new ConfigRepository();
        }


        private bool ChangeCredentials(string user, string password)
        {
            bool userUpdated = _configRepository.Actualizar("USER", user);
            bool passUpdated = _configRepository.Actualizar("PASSWORD", password);

            return userUpdated && passUpdated;
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string passwordActualDb = _configRepository.Obtener("PASSWORD");

            if (string.IsNullOrWhiteSpace(txtActual.Text) ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show(
                 "Todos los campos son obligatorios.",
                 "Validación",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning
             );
                return;
            }

            if (txtActual.Text != passwordActualDb)
            {
                MessageBox.Show("La contraseña actual no coincide.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtActual.Clear();
                txtActual.Focus();
                return;
            }

            bool actualizado = ChangeCredentials(
                txtUsuario.Text.Trim(),
                txtContrasena.Text.Trim()
            );

            if (actualizado)
            {
                MessageBox.Show(
                  "Credenciales actualizadas correctamente.",
                  "Operación exitosa",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information
              );
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar la información.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
