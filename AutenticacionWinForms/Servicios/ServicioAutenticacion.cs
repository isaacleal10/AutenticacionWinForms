using AutenticacionWinForms.Modelos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionWinForms.Servicios
{
    public class ServicioAutenticacion
    {
        private readonly HttpClient _clienteHttp;

        private const string UrlToken = "http://localhost:8080/realms/MiProyecto/protocol/openid-connect/token";
        private const string ClienteId = "frontend-app";
        private const string ClienteSecret = "tu-secret";
        private const string Alcance = "openid";

        public ServicioAutenticacion()
        {
            _clienteHttp = new HttpClient();
        }

        public async Task<TokenRespuesta> AutenticarAsync(Credenciales credenciales)
        {
            var datos = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", ClienteId },
                { "client_secret", ClienteSecret },
                { "scope", Alcance },
                { "username", credenciales.Usuario },
                { "password", credenciales.Clave }
            };

            var contenido = new FormUrlEncodedContent(datos);
            var respuesta = await _clienteHttp.PostAsync(UrlToken, contenido);

            respuesta.EnsureSuccessStatusCode();

            var json = await respuesta.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenRespuesta>(json);
        }
    }
}
