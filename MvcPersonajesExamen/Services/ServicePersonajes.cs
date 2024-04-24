using MvcPersonajesExamen.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcPersonajesExamen.Services
{
    public class ServicePersonajes
    {
        private MediaTypeWithQualityHeaderValue header;
        private string ApiUrlPersonajes;

        public ServicePersonajes (IConfiguration configuration)
        {
            this.ApiUrlPersonajes = 
                configuration.GetValue<string>("ApiUrls:ApiUrlPersonajes");
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T> CallApisAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content
                        .ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes/GetPersonajes";
            List<Personaje> personajes = await
                this.CallApisAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/Series";

            List<string> series = await
                this.CallApisAsync<List<string>>(request);

            return series;
        }

        public async Task<List<Personaje>>
            GetPersonajesSerieAsync(string serie)
        {
            string request = "api/Personajes/PersonajesSerie/" + serie;
            List<Personaje> personajes = await
                this.CallApisAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje>
            FindPersonajeAsync(int id)
        {
            string request = "api/Personajes/FindPersonaje/" + id;
            Personaje personaje = await
                this.CallApisAsync<Personaje>(request);
            return personaje;
        }

        public async Task CrearPersonajeAsync
            (int id, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/InsertPersonaje";
                client.BaseAddress = new Uri(this.ApiUrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                Personaje p = new Personaje();
                p.IdPersonaje = id;
                p.Nombre = nombre;
                p.Imagen = imagen;
                p.Serie = serie;

                string json = JsonConvert.SerializeObject(p);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task EditarPersonajeAsync
            (int id, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/UpdatePersonaje";
                client.BaseAddress = new Uri(this.ApiUrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                Personaje p = new Personaje();
                p.IdPersonaje = id;
                p.Nombre = nombre;
                p.Imagen = imagen;
                p.Serie = serie;

                string json = JsonConvert.SerializeObject(p);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/DeletePersonaje/" + id;
                client.BaseAddress = new Uri(this.ApiUrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
    }
}
