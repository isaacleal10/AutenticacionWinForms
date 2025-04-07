using AutenticacionWinForms.Modelos;
using AutenticacionWinForms.Servicios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutenticacionWinForms.Formularios
{
    public partial class FormProductos : Form
    {
        private readonly TokenRespuesta _token;
        private List<Producto> _productos = new List<Producto>();


        public FormProductos(TokenRespuesta token)
        {
            InitializeComponent();
            _token = token;
            this.Load += FormProductos_Load;
        }

        private async void FormProductos_Load(object sender, EventArgs e)
        {
            await CargarProductosAsync();
        }

        private async Task CargarProductosAsync()
        {
            try
            {
                var servicio = new ServicioProducto(_token.TokenAcceso);
                _productos = await servicio.ObtenerProductosAsync();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = _productos;
                dgvProductos.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private Producto ObtenerProductoSeleccionado()
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is Producto producto)
                return producto;
            return null;
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var formDetalle = new FormProductoDetalle(_token);
            if (formDetalle.ShowDialog() == DialogResult.OK)
                await CargarProductosAsync();
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            var productoSeleccionado = ObtenerProductoSeleccionado();
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto para modificar.");
                return;
            }

            var formDetalle = new FormProductoDetalle(_token, productoSeleccionado);
            if (formDetalle.ShowDialog() == DialogResult.OK)
                await CargarProductosAsync();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var productoSeleccionado = ObtenerProductoSeleccionado();
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Está seguro de eliminar el producto '{productoSeleccionado.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmar == DialogResult.Yes)
            {
                try
                {
                    var servicio = new ServicioProducto(_token.TokenAcceso);
                    await servicio.EliminarProductoAsync(productoSeleccionado.Id);
                    await CargarProductosAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
            }
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarProductosAsync();
        }
    }
}
