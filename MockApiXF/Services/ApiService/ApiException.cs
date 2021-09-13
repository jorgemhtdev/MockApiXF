namespace MockApiXF.Services
{
    using System;
    using System.Net;

    public class ApiException : Exception
    {
        public Exception Exception { get; set; }

        public string Content { get; set; }

        public string Json { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Uri { get; set; }
    }
}
