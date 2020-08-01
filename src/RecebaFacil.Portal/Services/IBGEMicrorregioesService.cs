using Newtonsoft.Json;
using RecebaFacil.Portal.Services.Interfaces;
using RecebaFacil.Portal.Services.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Services
{
    public class IBGEMicrorregioesService : IIBGEMicrorregioesService
    {
        public async Task<IEnumerable<Microrregioes>> ObterTodos()
        {
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados/sp/municipios");
            return JsonConvert.DeserializeObject<IEnumerable<Microrregioes>>(await response.Content.ReadAsStringAsync());
        }
    }
}

namespace RecebaFacil.Portal.Services.Models
{
    public struct Microrregioes
    {
        public Microrregioes(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        [JsonPropertyName("id")]
        public string Id { get; private set; }
        [JsonPropertyName("nome")]
        public string Nome { get; private set; }
    }
}
