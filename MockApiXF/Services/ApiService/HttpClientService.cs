namespace MockApiXF.Services
{
    using MockApiXF.Extension;
    using MockApiXF.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Xamarin.Essentials;

    public sealed class HttpClientService
    {
        private readonly HttpClient httpClient;
        private readonly static HttpClientService _instance = new HttpClientService();

        private CancellationTokenSource cts;

        private readonly string hostMock = "https://YOURID.mock.pstmn.io/";
        private readonly string hostMockError = "https://httpstat.us/";
        private readonly string hostDev = $"https://raw.githubusercontent.com/jorgemhtdev/comic-json/master/";
        private readonly string hostPre = string.Empty;
        private readonly string hostPro = string.Empty;

        private JsonSerializerSettings jsonSerializerSettings;

        public string Token { get; set; } = string.Empty;

        public static HttpClientService Instance
        {
            get => _instance;
        }

        public HttpClientService()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(hostDev)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public async Task<ResponseService<Tout>> Get<Tout>(string uri, NameValueCollection? nvc, TimeSpan timeout, bool auth = true)
        {
            ResponseService<Tout> responseService = new ResponseService<Tout>();

            try
            {
                if(nvc != null)
                {
                    uri += ToQueryString(nvc);
                }

                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);

                HttpResponseMessage response = await httpClient.GetAsync(uri, cts.Token)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    responseService.IsSuccessStatusCode = true;
                    responseService.ToModel(data);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw responseService.error = new ApiException()
                    {
                        Content = data,
                        StatusCode = response.StatusCode,
                        Uri = $"Get: {uri}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();

                ElevarOperationCanceledException(operationCanceledException, $"Get: {uri}");
            }
            catch (ApiException apiException)
            {
                ElevarApiExceptionException(apiException);
            }
            catch (Exception exception)
            {
                ElevarException(exception, $"Get: {uri}");
            }
            finally
            {
                cts.Dispose();
            }

            return responseService;
        }

        public async Task<ResponseService<Tout>> Post<TIn, Tout>(TIn root, string uri, TimeSpan timeout, bool auth = true)
        {
            ResponseService<Tout> outObject = new ResponseService<Tout>();
            ResponseService<TIn> inObject = new ResponseService<TIn>();

            try
            {
                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                inObject.model = root;

                StringContent content = new StringContent(inObject.ToString(), Encoding.UTF8, "application/json");

                cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);

                HttpResponseMessage response = await httpClient.PostAsync(uri, content, cts.Token)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    outObject.IsSuccessStatusCode = true;
                    outObject.StatusCode = response.StatusCode;
                    outObject.ToModel(data);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw outObject.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Uri = $"Post: {uri}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();

                ElevarOperationCanceledException(operationCanceledException, $"Post: {uri}");
            }
            catch (ApiException apiException)
            {
                ElevarApiExceptionException(apiException);
            }
            catch (Exception exception)
            {
                ElevarException(exception, $"Post: {uri}");
            }
            finally
            {
                cts.Dispose();
            }

            return outObject;
        }

        public async Task<ResponseService<Tout>> Update<Tout>(string uri, TimeSpan timeout, bool auth = false)
        {
            ResponseService<Tout> responseService = new ResponseService<Tout>();

            try
            {
                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);

                HttpResponseMessage response = await httpClient.PutAsync(uri, null, cts.Token)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode == true)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    responseService.IsSuccessStatusCode = true;
                    responseService.model = JsonConvert.DeserializeObject<Tout>(data, jsonSerializerSettings);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw responseService.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Uri = $"Update: {uri}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();

                ElevarOperationCanceledException(operationCanceledException, $"Update: {uri}");
            }
            catch (ApiException apiException)
            {
                ElevarApiExceptionException(apiException);
            }
            catch (Exception exception)
            {
                ElevarException(exception, $"Update: {uri}");
            }
            finally
            {
                cts.Dispose();
            }

            return responseService;
        }

        public async Task<ResponseService<Tout>> Update<Tout>(Tout root, string uri, TimeSpan timeout, bool auth = false)
        {
            ResponseService<Tout> responseService = new ResponseService<Tout>();

            try
            {
                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                responseService.model = root;

                StringContent content = new StringContent(responseService.ToString(), Encoding.UTF8, "application/json");

                cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);

                HttpResponseMessage response = await httpClient.PutAsync(uri, content, cts.Token)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode == true)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    responseService.IsSuccessStatusCode = true;
                    responseService.model = JsonConvert.DeserializeObject<Tout>(data, jsonSerializerSettings);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw responseService.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Json = responseService.ToString(),
                        Uri = $"Update: {uri}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();

                ElevarOperationCanceledException(operationCanceledException, $"Update: {uri}");
            }
            catch (ApiException apiException)
            {
                ElevarApiExceptionException(apiException);
            }
            catch (Exception exception)
            {
                ElevarException(exception, $"Update: {uri}");
            }
            finally
            {
                cts.Dispose();
            }

            return responseService;
        }

        public async Task<ResponseService<bool>> Delete(string uri, CancellationToken cancellationToken, bool auth = false)
        {
            ResponseService<bool> responseService = new ResponseService<bool>();

            try
            {
                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                HttpResponseMessage response = await httpClient.DeleteAsync(uri)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    responseService.model = true;
                    responseService.IsSuccessStatusCode = true;
                    return responseService;
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    responseService.model = false;

                    throw responseService.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Uri = $"Delete: {uri}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();

                ElevarOperationCanceledException(operationCanceledException, $"Delete: {uri}");
            }
            catch (ApiException apiException)
            {
                ElevarApiExceptionException(apiException);
            }
            catch (Exception exception)
            {
                ElevarException(exception, $"Delete: {uri}");
            }
            finally
            {
                cts.Dispose();
            }

            return responseService;
        }

        public async Task<bool> PingAsync()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Uri hostUrl = new Uri("https://www.google.com/");

                    Ping ping = new Ping();

                    PingReply result = await ping.SendPingAsync(hostUrl.Host, 500);
                    return result.Status == IPStatus.Success;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(ExcepcionesEnum.ErrorSinConexion.ToDescriptionString(), exception);
            }
        }

        private Exception ElevarOperationCanceledException(OperationCanceledException operationCanceledException, string uri)
        {
            throw new Exception(ExcepcionesEnum.TimeOut.ToDescriptionString(), new ApiException()
            {
                Exception = operationCanceledException,
                Uri = uri
            });
        }

        private Exception ElevarApiExceptionException(ApiException apiException)
        {
            switch (apiException.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    throw new Exception(ErrorApiEnum.Error400BadRequest.ToDescriptionString(), apiException);

                case System.Net.HttpStatusCode.NotFound:
                    throw new Exception(ErrorApiEnum.Error403Forbidden.ToDescriptionString(), apiException);

                case System.Net.HttpStatusCode.RequestTimeout:
                    throw new Exception(ErrorApiEnum.Error408TimeOut.ToDescriptionString(), apiException);

                case System.Net.HttpStatusCode.InternalServerError:
                    throw new Exception(ErrorApiEnum.Error500InternalServerError.ToDescriptionString(), apiException);

                default:
                    throw new Exception(ExcepcionesEnum.Error.ToDescriptionString(), apiException);
            }
        }

        private Exception ElevarException(Exception exception, string uri)
        {
            throw new Exception(ExcepcionesEnum.Error.ToDescriptionString(), new ApiException()
            {
                Exception = exception,
                Uri = uri
            });
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            string[] array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select string.Format(
            "{0}={1}",
            HttpUtility.UrlEncode(key),
            HttpUtility.UrlEncode(value))
                ).ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
