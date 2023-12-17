namespace CurlThin
{
    using System;

    internal sealed class Finalizer
    {
        public Finalizer(Action action)
        {
            Action = action;
        }

        ~Finalizer()
        {
            Action.Invoke();
        }

        public Action Action { get; }
    }
}
