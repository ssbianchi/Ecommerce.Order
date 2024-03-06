using RestSharp;
using System.Text.Json;

namespace Eccomerce.Order.Rabbit.Consumer.WebApi
{
    public abstract class AbstractWebAPI
    {
        protected abstract RestClient Client { get; }
        protected abstract JsonSerializerOptions Options { get; }
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,                                                         // ignore upper-/lower-case
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // do not add NULL fields to Json
        };
        protected async Task<RestResponse> ExecuteResponseAsync(Method method, string url, params KeyValuePair<string, object>[] args)
        {
            return await ExecuteResponseAsync(method, url, null, args);
        }
        protected async Task<RestResponse> ExecuteResponseAsync(Method method, string url, object body, params KeyValuePair<string, object>[] args)
        {
            var clientOptions = new RestClientOptions(url) { ThrowOnAnyError = true, MaxTimeout = -1 };
            var client = new RestClient(clientOptions);

            // Request
            var request = new RestRequest() { Method = method };

            // Json Body
            if (body != null)
                request = request.AddJsonBody(body);

            // Parameters
            foreach (var arg in args)
            {
                var key = arg.Key;
                var value = arg.Value;

                if (value != null)
                {
                    string param;

                    if (value is DateTime dateParam)
                        param = $"{dateParam:yyyy-MM-dd}";
                    else if (value is string stringParam)
                        param = stringParam;
                    else
                        param = value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(param))
                        request = request.AddQueryParameter(key, param);
                }
            }

            // Execute
            RestResponse response = null;

            try
            {
                switch (method)
                {
                    case Method.Get:
                        response = await client.ExecuteGetAsync(request);
                        break;
                    case Method.Post:
                        response = await client.ExecutePostAsync(request);
                        break;
                    case Method.Put:
                        response = await client.ExecutePutAsync(request);
                        break;
                    case Method.Delete:
                        request.Method = Method.Delete;
                        response = await client.ExecuteAsync(request);
                        break;
                    default:
                        throw new Exception("Unknown method");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
        protected async Task<RestResponse> ExecuteResponseAsync(Method method, string url, params Param[] args)
        {
            return await ExecuteResponseAsync(method, url, null, args);
        }
        protected async Task<RestResponse> ExecuteResponseAsync(Method method, string url, object body, params Param[] args)
        {
            // Request
            var request = new RestRequest(url) { Method = method };

            // Json Body
            if (body != null)
                request = request.AddJsonBody(body);

            // Parameters
            foreach (var arg in args)
            {
                var key = arg.Key;
                var value = arg.Value;

                if (value != null)
                {
                    string param;

                    if (value is DateTime dateParam)
                        param = $"{dateParam:yyyy-MM-dd}";
                    else if (value is string stringParam)
                        param = stringParam;
                    else
                        param = value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(param))
                    {
                        switch (arg.ParamType)
                        {
                            case ParameterType.QueryString:
                                request = request.AddQueryParameter(key, param);
                                break;
                            case ParameterType.HttpHeader:
                                request = request.AddHeader(key, param);
                                break;
                            default:
                                request = request.AddParameter(key, param, arg.ParamType);
                                break;
                        }
                    }
                }
            }

            // Execute
            RestResponse response = null;
            try
            {
                switch (method)
                {
                    case Method.Get:
                        response = await Client.ExecuteGetAsync(request);
                        break;
                    case Method.Post:
                        response = await Client.ExecutePostAsync(request);
                        break;
                    case Method.Put:
                        response = await Client.ExecutePutAsync(request);
                        break;
                    case Method.Delete:
                        request.Method = Method.Delete;
                        response = await Client.ExecuteAsync(request);
                        break;
                    default:
                        throw new System.Exception("Unknown method");
                }
            }
            catch (System.Exception e)
            {
                throw;
            }

            return response;
        }
        protected async Task<T> ExecuteAsync<T>(Method method, string url, params Param[] args)
        {
            return await ExecuteAsync<T>(method, url, null, args);
        }
        protected async Task<T> ExecuteAsync<T>(Method method, string url, object body, params Param[] args)
        {
            RestResponse response = await ExecuteResponseAsync(method, url, body, args);

            // Response
            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.Created)
                return JsonSerializer.Deserialize<T>(response.Content, Options);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return default;

            if (response.ContentType?.StartsWith("application/problem+json") ?? false)
                throw new HttpRequestException(response.Content);

            // var fullUrl = client.BuildUri(request);
            // fullUrl.AbsoluteUri
            throw new System.Exception($"Error retrieving data from Api ({url}): ", response.ErrorException);
        }
        protected async Task<T> ExecuteAsync<T>(Method method, string url, params KeyValuePair<string, object>[] args)
        {
            return await ExecuteAsync<T>(method, url, null, args);
        }
        protected async Task<T> ExecuteAsync<T>(Method method, string url, object body, params KeyValuePair<string, object>[] args)
        {
            RestResponse response = await ExecuteResponseAsync(method, url, body, args);

            // Response
            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.Created)
                return JsonSerializer.Deserialize<T>(response.Content, _options);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return default(T);

            if (response.ContentType?.StartsWith("application/problem+json") ?? false)
                throw new Exceptions.HttpRequestException(response.Content);

            // var fullUrl = client.BuildUri(request);
            // fullUrl.AbsoluteUri
            throw new Exception($"Error retrieving data from Api ({url}): ", response.ErrorException);
        }
        protected KeyValuePair<string, object> CreateParam(string key, object value)
        {
            return new KeyValuePair<string, object>(key, value);
        }
        protected Param CreateParam(string key, object value, ParameterType paramType = ParameterType.QueryString)
        {
            return new Param(key, value, paramType);
        }
    }
    public class Param
    {
        public ParameterType ParamType { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
        public Param(string key, object value, ParameterType paramType)
        {
            Key = key;
            Value = value;
            ParamType = paramType;
        }
    }
}
