using System;

namespace Common.Common
{
    public class DisposeBase : IDisposable
    {
        private bool _disposed;

        ~DisposeBase()
        {
            Logger.Debug("开始自动销毁资源!");
            Dispose(false);
            Logger.Debug("自动销毁资源!");
        }

        private void Dispose(bool isDisposing)
        {
            Logger.Indent();
            var flag = "";//GetType().FullName;
            Logger.Debug($"开始销毁资源!({flag})");
            if (_disposed)
            {
                Logger.Debug("已经销毁过资源!");
                return;
            }

            if (isDisposing)
            {
                Logger.Debug("开始销毁托管资源!");
                DisposeManagedResources();
                Logger.Debug("销毁托管资源完毕!");
            }

            Logger.Debug("开始销毁非托管资源!");
            DisposeUnManagedResources();
            Logger.Debug("销毁非托管资源完毕!");

            _disposed = true;
            Logger.Debug($"销毁资源完毕!({flag})");
            Logger.UnIndent();
        }

        public virtual void Dispose()
        {
            Logger.Debug("开始手动销毁资源!");
            Dispose(true);
            GC.SuppressFinalize(this);
            Logger.Debug("手动销毁资源完毕!");
        }

        protected virtual void DisposeUnManagedResources()
        {
        }

        protected virtual void DisposeManagedResources()
        {
        }
    }
}