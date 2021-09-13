namespace MockApiXF.Base
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Reflection;

    public class ModelBase
    {
        protected T LoadJson<T>(JsonEnum nameFile) where T : class
        {
            var assembly = typeof(ModelBase).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{nameFile}.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }

    public enum JsonEnum
    {
        Comics
    }
}
