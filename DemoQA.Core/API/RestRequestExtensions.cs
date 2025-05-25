using RestSharp;

namespace DemoQA.Core.API
{
    public static class RestRequestExtensions
    {
        public static RestRequest AddDefaultHeaders(this RestRequest request)
        {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        public static RestRequest AddAuthHeader(this RestRequest request, string token, string type = "Bearer")
        {
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"{type} {token}");
            }
            return request;
        }

        public static RestRequest AddJsonPayload<T>(this RestRequest request, T body) where T : class
        {
            if (body != null)
            {
                request.AddJsonBody(body);
            }
            return request;
        }

        public static RestRequest AddQueryParam(this RestRequest request, string name, string? value)
        {
            if (value != null)
            {
                request.AddQueryParameter(name, value);
            }
            return request;
        }

        public static RestRequest AddUrlSegmentParam(this RestRequest request, string name, string? value)
        {
            if (value != null)
            {
                request.AddUrlSegment(name, value);
            }
            return request;
        }
    }
}
