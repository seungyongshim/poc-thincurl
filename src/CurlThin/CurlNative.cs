namespace CurlThin
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public static class CurlNative
    {
        static CurlNative()
        {
            var myPath = new Uri(typeof(CurlNative).Assembly.CodeBase).LocalPath;
            var myFolder = Path.GetDirectoryName(myPath);

            var is64 = IntPtr.Size == 8;
            var subfolder = is64 ? "\\win64\\" : "\\win32\\";

            LoadLibrary(myFolder + subfolder + LIBCURL);
        }


        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        public const string LIBCURL = "libcurl";

        [DllImport(LIBCURL, EntryPoint = "curl_global_init")]
        public static extern CURLcode Init(CURLglobal flags = CURLglobal.DEFAULT);

        [DllImport(LIBCURL, EntryPoint = "curl_global_cleanup")]
        public static extern void Cleanup();

        public static class Slist
        {
            [DllImport(LIBCURL, EntryPoint = "curl_slist_append")]
            public static extern SafeSlistHandle Append(SafeSlistHandle slist, string data);

            [DllImport(LIBCURL, EntryPoint = "curl_slist_free_all")]
            public static extern void FreeAll(SafeSlistHandle pList);
        }

        public static class Easy
        {
            public delegate UIntPtr DataHandler(IntPtr data, UIntPtr size, UIntPtr nmemb, IntPtr userdata);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_init")]
            public static extern SafeEasyHandle Init();

            [DllImport(LIBCURL, EntryPoint = "curl_easy_cleanup")]
            public static extern void Cleanup(IntPtr handle);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_perform")]
            public static extern CURLcode Perform(SafeEasyHandle handle);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_reset")]
            public static extern void Reset(SafeEasyHandle handle);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_setopt")]
            public static extern CURLcode SetOpt(SafeEasyHandle handle, CURLoption option, int value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_setopt")]
            public static extern CURLcode SetOpt(SafeEasyHandle handle, CURLoption option, IntPtr value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_setopt", CharSet = CharSet.Ansi)]
            public static extern CURLcode SetOpt(SafeEasyHandle handle, CURLoption option, string value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_setopt", CharSet = CharSet.Unicode)]
            public static extern CURLcode SetOptUnicode(SafeEasyHandle handle, CURLoption option, string value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_setopt")]
            public static extern CURLcode SetOpt(SafeEasyHandle handle, CURLoption option, DataHandler value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_getinfo")]
            public static extern CURLcode GetInfo(SafeEasyHandle handle, CURLINFO option, out int value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_getinfo")]
            public static extern CURLcode GetInfo(SafeEasyHandle handle, CURLINFO option, out IntPtr value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_getinfo")]
            public static extern CURLcode GetInfo(SafeEasyHandle handle, CURLINFO option, out double value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_getinfo", CharSet = CharSet.Ansi)]
            public static extern CURLcode GetInfo(SafeEasyHandle handle, CURLINFO option, IntPtr value);

            [DllImport(LIBCURL, EntryPoint = "curl_easy_strerror")]
            public static extern IntPtr StrError(CURLcode errornum);
        }
    }
}
