using System;

namespace Denmakers.DreamSale.Data.Infrastructure
{
    public class Disposable : IDisposable
    {
        private bool isDisposed;
        
        // Ovveride this to dispose custom objects
        protected virtual void DisposeCore()
        {
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }
        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
