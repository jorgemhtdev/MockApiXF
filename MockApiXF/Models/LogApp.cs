namespace MockApiXF.Models
{
    using System;

    public class LogApp
    {
        public int Id { get; set; }

        public string Plataforma { get; set; }

        public string VersionDispositivo { get; set; }

        public string TipoDispositivo { get; set; }

        public bool Simulador { get; set; }

        public string Modelo { get; set; }

        public string Marca { get; set; }

        public string Mensaje { get; set; }

        public LangEnum Idioma { get; set; }

        public string App { get; set; }

        public string VersionApp { get; set; }

        public string Orientacion { get; set; }

        public DateTime Fecha { get; set; }

        public bool Sincronizado { get; set; }

        public Exception Excepcion { get; set; }
    }
}
