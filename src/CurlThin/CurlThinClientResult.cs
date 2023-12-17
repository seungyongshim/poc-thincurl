namespace CurlThin
{
    public class CurlThinClientResult
    {
        public CURLcode CurlResult { get; set; }
        public int StatusCode { get; set; }
        public string JsonBody { get; set; }
    }
}
