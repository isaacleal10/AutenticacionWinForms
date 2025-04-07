using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionWinForms.Modelos
{
    public class TokenRespuesta
    {
        [Newtonsoft.Json.JsonProperty("access_token")]
        public string TokenAcceso { get; set; }

        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int ExpiraEn { get; set; }

        [Newtonsoft.Json.JsonProperty("token_type")]
        public string TipoToken { get; set; }

        [Newtonsoft.Json.JsonProperty("refresh_token")]
        public string TokenRefresco { get; set; }
    }
}
