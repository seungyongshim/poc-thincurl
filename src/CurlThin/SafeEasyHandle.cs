namespace CurlThin
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class SafeEasyHandle : SafeHandle
    {
        private SafeEasyHandle() : base(IntPtr.Zero, false)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            CurlNative.Easy.Cleanup(handle);
            return true;
        }
    }
}
