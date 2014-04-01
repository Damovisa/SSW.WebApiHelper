using System;
using System.Net;

namespace SSW.WebApiHelper
{
    public class WebApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }

        public WebApiException(HttpStatusCode statusCode, string content, string url, Exception innerException)
            : base(string.Format("Url: {0}, Status Code: {1}, Content: {2}", url, statusCode, content), innerException)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
