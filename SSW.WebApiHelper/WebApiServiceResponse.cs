using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SSW.WebApiHelper
{
    public class WebApiServiceResponse
    {
        public string Content { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public WebApiServiceResponse(IRestResponse response)
        {
            Content = response.Content;
            StatusCode = response.StatusCode;
        }
    }

    public class WebApiServiceResponse<T>
    {
        public string Content { get; private set; }
        public T Data { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public WebApiServiceResponse(IRestResponse response)
        {
            Content = response.Content;
            Data = JsonConvert.DeserializeObject<T>(response.Content);
            StatusCode = response.StatusCode;
        }
    }
}