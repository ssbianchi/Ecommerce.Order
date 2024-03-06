using Eccomerce.Order.Rabbit.Consumer.WebApi.Json;
using RestSharp;
using System.Text.Json;

namespace Eccomerce.Order.Rabbit.Consumer.WebApi.Order
{
    public class OrderAPI : AbstractWebAPI, IOrderAPI
    {
        private readonly string BaseUrl;
        public OrderAPI()
        {
            BaseUrl = "http://localhost:5282/api/Order";
        }
        private static readonly object _object = new object();
        private static readonly object _object2 = new object();

        private static RestClient _client = null;
        protected override RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (_object)
                    {
                        if (_client == null)
                        {
                            var clientOptions = new RestClientOptions(BaseUrl)
                            {
                                MaxTimeout = -1,
                                ThrowOnAnyError = true
                            };
                            _client = new RestClient(clientOptions);
                        }
                    }
                }
                return _client;
            }
        }
        private static JsonSerializerOptions _options = null;
        protected override JsonSerializerOptions Options
        {
            get
            {
                if (_options == null)
                {
                    lock (_object2)
                    {
                        if (_options == null)
                        {
                            _options = new JsonSerializerOptions()
                            {
                                PropertyNameCaseInsensitive = true,
                                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                                WriteIndented = true,
                            };
                            _options.Converters.Add(new ObjectToInferredTypesConverter());
                        }
                    }
                }
                return _options;
            }
        }
        public async Task<bool> CloseOrderSession(int orderSessionId, int statusId)
        {
            try
            {
                var url = $"{BaseUrl}/CloseOrderSession";
                var args = new List<Param>
                {
                    CreateParam("orderSessionId", orderSessionId,ParameterType.QueryString),
                    CreateParam("orderSessionStatusId", statusId, ParameterType.QueryString),
                };

                return await ExecuteAsync<bool>(Method.Post, url, args.ToArray());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
