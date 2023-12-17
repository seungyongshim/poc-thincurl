namespace CurlThin
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;

    public class CurlThinClient
    {
        static readonly Finalizer s_finalizer;

        static CurlThinClient()
        {
            CurlNative.Init();
            s_finalizer = new Finalizer(() => CurlNative.Cleanup());
        }

        static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
        };

        public CurlThinClientResult Post(string url, object data, string proxy = null)
        {
            var json = JsonConvert.SerializeObject(data, JsonSerializerSettings);

            using (var dataCopier = new DataCallbackCopier())
            using (var easy = CurlNative.Easy.Init())
            {
                if (!string.IsNullOrEmpty(proxy))
                {
                    CurlNative.Easy.SetOpt(easy, CURLoption.PROXY, proxy);
                }
                
                CurlNative.Easy.SetOpt(easy, CURLoption.URL, url);
                CurlNative.Easy.SetOpt(easy, CURLoption.SSL_VERIFYPEER, 0);
                CurlNative.Easy.SetOpt(easy, CURLoption.COPYPOSTFIELDS, json);
                using (var headers = CurlNative.Slist.Append(SafeSlistHandle.Null, "Content-Type: application/json"))
                {
                    CurlNative.Slist.Append(headers, "Accept: application/json");
                    CurlNative.Easy.SetOpt(easy, CURLoption.HTTPHEADER, headers.DangerousGetHandle());
                    CurlNative.Easy.SetOpt(easy, CURLoption.WRITEFUNCTION, dataCopier.DataHandler);


                    var result = CurlNative.Easy.Perform(easy);
                    
                    CurlNative.Easy.GetInfo(easy, CURLINFO.RESPONSE_CODE, out int statusCode);

                    return new CurlThinClientResult
                    {
                        CurlResult = result,
                        StatusCode = statusCode,
                        JsonBody = statusCode > 0 ? Encoding.UTF8.GetString(dataCopier.Stream.ToArray()) : string.Empty
                    };
                }
            }
        }
    }
}
