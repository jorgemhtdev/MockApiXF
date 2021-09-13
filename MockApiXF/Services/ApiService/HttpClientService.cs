namespace MockApiXF.Services
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Xamarin.Essentials;

    public sealed class HttpClientService
    {
        private readonly HttpClient httpClient;
        private readonly static HttpClientService _instance = new HttpClientService();

        private CancellationTokenSource cts;

        private readonly string hostMock = "https://httpstat.us/";
        private readonly string hostMockError = "https://httpstat.us/";
        private readonly string hostDev = string.Empty;
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
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            // Ignorarlos null, 
            // 
            // ignorar para que los valores de bucle se excluyan de la serialización en lugar de lanzar una excepción.
            // https://stackoverflow.com/questions/23453977/what-is-the-difference-between-preservereferenceshandling-and-referenceloophandl/23461179   
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
            ResponseService<Tout> jr = new ResponseService<Tout>();

            try
            {
                uri = ToQueryString(nvc);

                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);

                HttpResponseMessage response = await httpClient.GetAsync(uri)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    jr.IsSuccessStatusCode = true;
                    jr.ToModel(data);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw jr.error = new ApiException()
                    {
                        Content = Regex.Unescape(data),
                        StatusCode = response.StatusCode,
                        Uri = $"Get: {uri}. {Regex.Unescape(data)}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();
                throw operationCanceledException;
            }
            catch (ApiException apiException)
            {
                throw apiException;
            }
            catch (Exception exception)
            {
                throw jr.error = new ApiException()
                {
                    Exception = exception,
                    Uri = $"Get: {uri}"
                };
            }
            finally
            {
                cts.Dispose();
            }

            return jr;
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
                        Uri = $"Post: {uri}. {Regex.Unescape(data)}"
                    };
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();
                throw operationCanceledException;
            }
            catch (ApiException apiException)
            {
                throw apiException;
            }
            catch (Exception exception)
            {
                throw outObject.error = new ApiException()
                {
                    Exception = exception,
                    Uri = $"Get: {uri}"
                };
            }
            finally
            {
                cts.Dispose();
            }

            return outObject;
        }

        public async Task<ResponseService<Tout>> Update<Tout>(string uri, TimeSpan timeout, bool auth = false)
        {
            ResponseService<Tout> jr = new ResponseService<Tout>();

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
                    jr.IsSuccessStatusCode = true;
                    jr.model = JsonConvert.DeserializeObject<Tout>(data, jsonSerializerSettings);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw jr.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Uri = $"Update: {uri}. {Regex.Unescape(data)}"
                    };
                }
            }
            catch (ApiException apiException)
            {
                throw apiException;
            }
            catch (Exception exception)
            {
                jr.error.Uri = $"Update: {uri}. {exception.ToString()}";
                throw jr.error;
            }
            finally
            {
                cts.Cancel();
            }

            return jr;
        }

        public async Task<ResponseService<Tout>> Update<Tout>(Tout root, string uri, TimeSpan timeout, bool auth = false)
        {
            ResponseService<Tout> jr = new ResponseService<Tout>();

            try
            {
                if (auth)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }

                jr.model = root;

                StringContent content = new StringContent(jr.ToString(), Encoding.UTF8, "application/json");

                cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);

                HttpResponseMessage response = await httpClient.PutAsync(uri, content, cts.Token)
                                                           .ConfigureAwait(false);

                if (response.IsSuccessStatusCode == true)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    jr.IsSuccessStatusCode = true;
                    jr.model = JsonConvert.DeserializeObject<Tout>(data, jsonSerializerSettings);
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    throw jr.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Json = jr.ToString(),
                        Uri = $"Update: {uri}. {Regex.Unescape(data)}"
                    };
                }
            }
            catch (ApiException apiException)
            {
                throw apiException;
            }
            catch (Exception exception)
            {
                jr.error.Uri = $"Update: {uri}. {exception.ToString()}";
                throw jr.error;
            }
            finally
            {
                cts.Cancel();
            }

            return jr;
        }

        public async Task<ResponseService<bool>> Delete(string uri, CancellationToken cancellationToken, bool auth = false)
        {
            ResponseService<bool> jr = new ResponseService<bool>();

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
                    jr.model = true;
                    jr.IsSuccessStatusCode = true;
                    return jr;
                }
                else
                {
                    string data = await response.Content.ReadAsStringAsync();
                    jr.model = false;

                    throw jr.error = new ApiException()
                    {
                        Content = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode,
                        Uri = $"Delete: {uri}. {Regex.Unescape(data)}"
                    };
                }
            }
            catch (ApiException apiException)
            {
                throw apiException;
            }
            catch (Exception exception)
            {
                jr.error.Uri = $"Delete: {uri}. {exception.ToString()}";
                throw jr.error;
            }
            finally
            {
                cts.Cancel();
            }
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
                return false;
            }
        }
    }
}