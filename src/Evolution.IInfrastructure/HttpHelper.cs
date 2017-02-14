using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using JWT.Common;
using Newtonsoft.Json;

namespace Evolution.IInfrastructure
{
    public class HttpHelper
    {
        string rootUrl = null;
        public HttpHelper(IConfiguration config)
        {
            rootUrl = config["ApiServerBaseUrl"];
        }

        #region Get
        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public string Get(string url, Hashtable headht = null,string baseUrl=null)
        {
            HttpWebRequest request;
            if(baseUrl==null)
            {
                baseUrl = rootUrl;
            }

            url = Path.Combine(baseUrl, url);

            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //request = WebRequest.Create(url) as HttpWebRequest;
                //request.ProtocolVersion = HttpVersion.Version10;
                request = null;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            //request.Timeout = 15000;
            //request.AllowAutoRedirect = false;
            WebResponse response = null;
            string responseStr = null;
            if (headht != null)
            {
                foreach (DictionaryEntry item in headht)
                {
                    request.Headers[item.Key.ToString()] = item.Value.ToString();
                }
            }

            try
            {
                response = request.GetResponseAsync().Result;

                if (response != null)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseStr;
        }
        /// <summary>
        /// 用于js穿透访问后端API服务
        /// </summary>
        /// <param name="oldRequest"></param>
        /// <returns></returns>
        public async Task<string> GetResponseFromNewRequest(HttpRequest oldRequest,string tokenStr)
        {
            string token = tokenStr;
            string url = UriHelper.GetDisplayUrl(oldRequest);
            var x = url.IndexOf("/", 7);
            var start = url.Substring(x);
            url = rootUrl+start;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = null;
                //copy header
                client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);
                client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                switch (oldRequest.Method)
                {
                    case "POST": {
                            //copy content
                            string data = GetDocumentContents(oldRequest);
                            HttpContent postContent = new StringContent(data,Encoding.UTF8, "application/x-www-form-urlencoded");
                            response = await client.PostAsync(url, postContent);
                            break;
                        }
                    case "GET": {
                            response = await client.GetAsync(url);
                            break;
                        }
                    default:
                        throw (new Exception("unsupport http methord :"+oldRequest.Method));
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return responseJson;
                }
                else
                {
                    throw new HttpRequestException("Error:HttpCode is " + response.StatusCode.ToString());
                }
            }
        }

        #region POST
        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="param">POST的数据</param>
        /// <returns></returns>
        public async Task<string> HttpPost(string url, Dictionary<string, string> data = null, string baseUrl = null,Dictionary<string,string> headers=null)
        {
            if (baseUrl == null)
            {
                baseUrl = rootUrl;
            }
            url = baseUrl + url;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                if(headers!=null)
                {
                    foreach (string k in headers.Keys)
                        client.DefaultRequestHeaders.Add(k, headers[k]);
                }


                HttpContent ct = new FormUrlEncodedContent(data);
                var response = await client.PostAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return responseJson;
                }
                else
                {
                    throw new HttpRequestException("Error:HttpCode is " + response.ToString());
                }
            }
            //HttpWebRequest request;
            //if (baseUrl == null)
            //{
            //    baseUrl = rootUrl;
            //}

            //url = baseUrl+ url;

            ////如果是发送HTTPS请求  
            //if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            //{
            //    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //    //request = WebRequest.Create(url) as HttpWebRequest;
            //    //request.ProtocolVersion = HttpVersion.Version10;
            //    request = null;
            //}
            //else
            //{
            //    request = WebRequest.Create(url) as HttpWebRequest;
            //}

            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.Headers["X-Requested-With"] = "XMLHttpRequest";
            //request.Accept = "*/*";
            

            //StreamWriter requestStream = null;
            //WebResponse response = null;
            //string responseStr = null;

            //try
            //{
            //    requestStream = new StreamWriter(request.GetRequestStreamAsync().Result);
            //    requestStream.Write(param);

            //    response = request.GetResponseAsync().Result;
            //    if (response != null)
            //    {
            //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            //        {
            //            responseStr = reader.ReadToEnd();
            //        }
                        
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    request = null;
            //    requestStream = null;
            //    response = null;
            //}

            //return responseStr;
        }

        #endregion

        public string HttpGet(string url, Encoding encodeing, Hashtable headht = null)
        {
            //HttpWebRequest request;

            ////如果是发送HTTPS请求  
            //if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            //{
            //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //    request = WebRequest.Create(url) as HttpWebRequest;
            //    request.ProtocolVersion = HttpVersion.Version10;
            //}
            //else
            //{
            //    request = WebRequest.Create(url) as HttpWebRequest;
            //}
            //request.Method = "GET";
            ////request.ContentType = "application/x-www-form-urlencoded";
            //request.Accept = "*/*";
            //request.Timeout = 15000;
            //request.AllowAutoRedirect = false;
            //WebResponse response = null;
            //string responseStr = null;
            //if (headht != null)
            //{
            //    foreach (DictionaryEntry item in headht)
            //    {
            //        request.Headers.Add(item.Key.ToString(), item.Value.ToString());
            //    }
            //}

            //try
            //{
            //    response = request.GetResponse();

            //    if (response != null)
            //    {
            //        StreamReader reader = new StreamReader(response.GetResponseStream(), encodeing);
            //        responseStr = reader.ReadToEnd();
            //        reader.Close();
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //return responseStr;
            return "";
        }
        #endregion
        private string GetDocumentContents(HttpRequest Request)
        {
            string documentContents;
            using (Stream receiveStream = Request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }
    }
    
}
