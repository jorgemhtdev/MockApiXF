namespace MockApiXF.Models
{
    using System.ComponentModel;

    public enum ErrorApiEnum
    {
        [Description("No se ha podido procesar la petición. Inténtelo más tarde.")]
        Error400BadRequest = 400,

        [Description("No tienes autorización para realizar está petición")]
        Error403Forbidden = 403,

        [Description("Está petición está tardando demasiado. Inténtelo más tarde.")]
        Error408TimeOut = 408,

        [Description("Error con el servidor. Inténtelo más tarde.")]
        Error500InternalServerError = 500
    }
}