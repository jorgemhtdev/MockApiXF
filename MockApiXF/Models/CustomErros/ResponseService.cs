namespace MockApiXF.Services
{
    using Newtonsoft.Json;
    using System.Net;

    public class ResponseService<T>
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public T model { get; set; }

        public ApiException error { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ResponseService()
        {
            model = default(T);
            error = null;
            IsSuccessStatusCode = false;

            jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public void ToModel(string json)
        {
            model = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(model, jsonSerializerSettings);
        }
    }
}
