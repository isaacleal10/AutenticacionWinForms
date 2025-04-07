using AutenticacionWinForms.Modelos;
using AutenticacionWinForms.Servicios;
using System;
using System.Windows.Forms;

namespace AutenticacionWinForms.Formularios
{
    public partial class FormProductoDetalle : Form
    {
        private readonly TokenRespuesta _token;
        private readonly Producto _producto;

        public FormProductoDetalle(TokenRespuesta token, Producto producto = null)
        {
            InitializeComponent();
            _token = token;
            _producto = producto;
        }

        private void FormProductoDetalle_Load(object sender, EventArgs e)
        {
            if (_producto != null)
            {
                txtNombre.Text = _producto.Nombre;
                txtPrecio.Text = _producto.Precio.ToString("0.##");
                this.Text = "Editar Producto";
            }
            else
            {
                this.Text = "Agregar Producto";
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var nombre = txtNombre.Text.Trim();
                var precioValido = decimal.TryParse(txtPrecio.Text, out decimal precio);

                if (string.IsNullOrWhiteSpace(nombre) || !precioValido || precio <= 0)
                {
                    MessageBox.Show("Por favor ingrese un nombre válido y un precio mayor a cero.");
                    return;
                }

                var servicio = new ServicioProducto(_token.TokenAcceso);

                if (_producto == null)
                {
                    // Agregar nuevo producto
                    await servicio.CrearProductoAsync(new Producto
                    {
                        Nombre = nombre,
                        Precio = precio
                    });
                }
                else
                {
                    // Editar producto existente
                    _producto.Nombre = nombre;
                    _producto.Precio = precio;

                    await servicio.ActualizarProductoAsync(_producto);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
