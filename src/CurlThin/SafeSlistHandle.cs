namespace CurlThin
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class SafeSlistHandle : SafeHandle
    {
        private SafeSlistHandle() : base(IntPtr.Zero, false)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        private bool disposedValue = false;
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ReleaseHandle();
                }

                disposedValue = true;
            }
            base.Dispose(disposing);
        }

        protected override bool ReleaseHandle()
        {
            if (!disposedValue)
            {
                CurlNative.Slist.FreeAll(this);
            }
            return disposedValue;
        }

        public static SafeSlistHandle Null => new SafeSlistHandle();
    }
}
