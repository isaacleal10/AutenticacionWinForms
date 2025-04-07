using AutenticacionWinForms.Modelos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionWinForms.Servicios
{
    public class ServicioProducto
    {
        private readonly HttpClient _clienteHttp;

        public ServicioProducto(string token)
        {
            _clienteHttp = new HttpClient();
            _clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            var respuesta = await _clienteHttp.GetAsync("https://localhost:7004/api/productos");
            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(json);
        }

        public async Task CrearProductoAsync(Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _clienteHttp.PostAsync("https://localhost:7004/api/productos", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _clienteHttp.PutAsync($"https://localhost:7004/api/productos/{producto.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task EliminarProductoAsync(int id)
        {
            var response = await _clienteHttp.DeleteAsync($"https://localhost:7004/api/productos/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
