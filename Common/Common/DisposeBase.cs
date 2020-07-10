using NLog;
using System;
using Common.Extensions;

namespace Common.Common
{
    public class DisposeBase : IDisposable
    {
        protected readonly Logger Logger;
        private bool _disposed;

        public DisposeBase()
        {
            Logger = this.GetLogger();
        }

        ~DisposeBase()
        {
            Logger.Trace("开始自动销毁资源!");
            Dispose(false);
            Logger.Trace("自动销毁资源!");
        }

        private void Dispose(bool isDisposing)
        {
            Logger.Trace("开始销毁资源!");
            if (_disposed)
            {
                Logger.Trace("已经销毁过资源!");
                return;
            }

            if (isDisposing)
            {
                try
                {
                    Logger.Trace("开始销毁托管资源!");
                    DisposeManagedResources();
                    Logger.Trace("销毁托管资源完毕!");
                }
                catch (Exception e)
                {
                    Logger.Fatal(e);
                }
            }

            try
            {
                Logger.Trace("开始销毁非托管资源!");
                DisposeUnManagedResources();
                Logger.Trace("销毁非托管资源完毕!");
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
            }

            _disposed = true;
            Logger.Trace("销毁资源完毕!");
        }

        public void Dispose()
        {
            Logger.Trace("开始手动销毁资源!");
            Dispose(true);
            GC.SuppressFinalize(this);
            Logger.Trace("手动销毁资源完毕!");
        }

        protected virtual void DisposeUnManagedResources()
        {
        }

        protected virtual void DisposeManagedResources()
        {
        }
    }
}