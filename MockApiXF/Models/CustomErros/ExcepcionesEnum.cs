namespace MockApiXF.Models
{
    using System.ComponentModel;

    public enum ExcepcionesEnum
    {
        [Description("Error con el servidor. Inténtelo más tarde.")]
        Error,
        [Description("Parece que no dispones de conexión a internet. Por favor revise su conexión")]
        ErrorSinConexion,
        [Description("No se ha podido proceder la petición")]
        ErrorDesconocido,
        [Description("Está petición está tardando demasiado. Inténtelo más tarde.")]
        TimeOut
    }
}
