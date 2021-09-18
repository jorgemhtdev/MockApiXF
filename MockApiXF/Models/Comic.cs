namespace MockApiXF.Models
{
    using MockApiXF.Base;
    using MockApiXF.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class Comic : ModelBase
    {
        private readonly bool mockData;

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("nombreOriginal")]
        public string NombreOriginal { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("fechaDeVenta")]
        public DateTime FechaDeVenta { get; set; }

        [JsonProperty("formato")]
        public string Formato { get; set; }

        [JsonProperty("numPag")]
        public int NumPag { get; set; }

        [JsonProperty("tamano")]
        public string Tamano { get; set; }

        [JsonProperty("peso")]
        public double Peso { get; set; }

        [JsonProperty("color")]
        public bool Color { get; set; }

        [JsonProperty("precio")]
        public double Precio { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        public ImageSource ProductImage
        {
            get => new Uri(Photo);
        }

        [JsonProperty("novedad")]
        public bool Novedad { get; set; }

        [JsonProperty("agotado")]
        public bool Agotado { get; set; }

        [JsonProperty("disponibilidad")]
        public bool Disponibilidad { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public Comic(bool mockData = false)
        {
            this.mockData = mockData;
        }

        public async Task<IEnumerable<Comic>> CargarComics()
        {
            ResponseService<IEnumerable<Comic>> result;

            if (mockData)
            {
                // Opcion A Cargar un Json
                //return LoadJson<IEnumerable<Comic>>(JsonEnum.Comics);

                // Opcion B Clase Static
                //return DataMock.CargarComics();

                // Opcion C Tirar de un servicio en la nube como el de postman
                result = await HttpClientService.Instance
                .Get<IEnumerable<Comic>>(EndPointApi.GetAllComic, null, TimeSpan.FromSeconds(1), false);

                // Extra  https://httpstat.us/400
                //NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                //queryString.Add("sleep", "2000");

                //result = await HttpClientService.Instance
                //    .Get<IEnumerable<Comic>>(EndPointApi.ExampleTimeOut, queryString, TimeSpan.FromSeconds(1), false);

                // o 
                //result = await HttpClientService.Instance
                //    .Get<IEnumerable<Comic>>(400, queryString, TimeSpan.FromSeconds(2), false);
            }
            else
            {
                // el /comics.json es para encontrar el fichero en github. Realmente si se tira de una API el /comics.json no se tendría que indicar.
                result = await HttpClientService.Instance
                .Get<IEnumerable<Comic>>($"{EndPointApi.GetAllComic}/comics.json" , null, TimeSpan.FromSeconds(10), false);
            }

            return result.model;

        }
    }
}