using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BitGo.Objects
{
    public class CallResult<T>
    {
        /// <summary>
        /// The data returned by the call
        /// </summary>
        public T Data { get; internal set; }
        /// <summary>
        /// An error if the call didn't succeed
        /// </summary>
        public Error Error { get; internal set; }
        /// <summary>
        /// Whether the call was successful
        /// </summary>
        public bool Success => Error == null;

        public CallResult(T data, Error error)
        {
            Data = data;
            Error = error;
        }
    }

    public class WebCallResult<T> : CallResult<T>
    {
        /// <summary>
        /// The status code of the response. Note that a OK status does not always indicate success, check the Success parameter for this.
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; set; }

        public Dictionary<string, string> ResponseHeaders { get; set; }

        public WebCallResult(HttpResponseMessage response, T data, Error error) : base(data, error)
        {
            if (response != null)
            {
                ResponseStatusCode = response?.StatusCode;
                ResponseHeaders = new Dictionary<string, string>();
                foreach (var header in response.Headers)
                {
                    ResponseHeaders.Add(header.Key, string.Join(";", header.Value));
                }
            }
        }

        public static WebCallResult<T> CreateErrorResult(Error error)
        {
            return new WebCallResult<T>(null, default(T), error);
        }

        public static WebCallResult<T> CreateErrorResult(HttpResponseMessage response, Error error)
        {
            return new WebCallResult<T>(response, default(T), error);
        }
    }
}
