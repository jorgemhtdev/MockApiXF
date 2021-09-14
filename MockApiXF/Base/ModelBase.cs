namespace MockApiXF.Base
{
    using MockApiXF.Extension;
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;

    public class ModelBase
    {
        private string folder = "Mocks";
        protected T LoadJson<T>(JsonEnum nameFile) where T : class
        {
            var assembly = typeof(ModelBase).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{folder}.{nameFile.ToDescriptionString()}.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }

    public enum JsonEnum
    {
        [Description("comics")]
        Comics
    }
}