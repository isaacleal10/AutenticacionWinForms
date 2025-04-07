using AutenticacionWinForms.Modelos;
using AutenticacionWinForms.Servicios;
using System;
using System.Windows.Forms;

namespace AutenticacionWinForms.Formularios
{
    public partial class FrmLogin : Form
    {
        private readonly ServicioAutenticacion _servicioAutenticacion;

        public FrmLogin()
        {
            InitializeComponent();
            _servicioAutenticacion = new ServicioAutenticacion();
        }

        private async void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            var credenciales = new Credenciales
            {
                Usuario = txtUsuario.Text,
                Clave = txtClave.Text
            };

            try
            {
                var token = await _servicioAutenticacion.AutenticarAsync(credenciales);

                
                MessageBox.Show("Autenticación exitosa");

                var FormProductos = new FormProductos(token);
                FormProductos.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Autenticación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
