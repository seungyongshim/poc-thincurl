namespace CurlThin
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class CURLINFOTYPE
    {
        public const uint STRING = 0x100000;
        public const uint LONG = 0x200000;
        public const uint DOUBLE = 0x300000;
        public const uint SLIST = 0x400000;
        public const uint PTR = 0x400000; // same as SLIST
        public const uint SOCKET = 0x500000;
        public const uint OFF_T = 0x600000;
        public const uint MASK = 0x0fffff;
        public const uint TYPEMASK = 0xf00000;
    }
}
