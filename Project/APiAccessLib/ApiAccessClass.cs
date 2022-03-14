
//ApiAccessClass is for accessing a webAPI
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace APiAccessLib
{
    public class ApiAccessClass
    {
        private static HttpClient CreateHttpClient(string uri)
        {
            string baseuri = WebConfigurationManager.AppSettings["BaseUrlAPI"];
            string Baseurl = baseuri + uri;

            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "dc8dfde0440e45dea79b2d9e53eeb714");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
            //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Base64String);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");

            return client;
        }
        private static HttpContent CreateHttpContent(object content, object attachment, bool bLowerCase = false)
        {
            object dataObj;

            if (bLowerCase)
            {
                if (attachment == null)
                    dataObj = new { data = content };
                else
                    dataObj = new { data = content, attachments = attachment };
            }
            else
            {
                if (attachment == null)
                    dataObj = new { Data = content };
                else
                    dataObj = new { Data = content, attachments = attachment };
            }


            string json = JsonConvert.SerializeObject(dataObj, Formatting.None);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
        /// <summary>
        public async Task<string> GetAsyn(string url)
        {
            string responseText = string.Empty;

            try
            {
                using (var client = CreateHttpClient(""))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    using (HttpResponseMessage Res = await client.GetAsync(url).ConfigureAwait(false))
                    {
                        //Checking the response is successful or not which is sent using HttpClient  
                        if (Res.IsSuccessStatusCode)
                        {
                            responseText = await Res.Content.ReadAsStringAsync();
                            //if (trimTableText && !String.IsNullOrEmpty(responseText))
                            //    responseText = responseText.Remove(0, 9).TrimEnd('}');

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return responseText;

        }
        public async Task<string> PostAsyncc(string url, object data, object attachment, bool bLowerCase = false)
        {
            using (var httpClient = CreateHttpClient(""))
            {
                //  string json = await Task.Run(() => JsonConvert.SerializeObject(data));
                var httpContent = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await httpClient.PostAsync(url, httpContent);
                }
                catch (Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }
                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                }
                return responseMessage.StatusCode.ToString();
            }
        }
        public async Task<string> PostAsyn(string url, object data, object attachment, bool bLowerCase = false)
        {
            string responseText = string.Empty;

            try
            {
                using (var client = CreateHttpClient(""))
                {


                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                    using (var httpContent = CreateHttpContent(data, null, bLowerCase))
                    {
                        request.Content = httpContent;

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            //Checking the response is successful or not which is sent using HttpClient  
                            if (response.IsSuccessStatusCode)
                            {
                                responseText = await response.Content.ReadAsStringAsync();
                                if (!String.IsNullOrEmpty(responseText) && responseText.Length > 9)
                                    responseText = responseText.Remove(0, 9).TrimEnd('}');

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseText;

        }


    }


}
