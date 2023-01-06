using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zhai.Famil.Common.Threads
{
    public class AloneTask
    {
        private CancellationTokenSource runningTokenSource;

        public Task Run(Action<CancellationTokenSource> action)
        {
            if (this.runningTokenSource != null)
            {
                this.runningTokenSource.Cancel();
                this.runningTokenSource = null;
            }

            var localTokenSource = new CancellationTokenSource();

            this.runningTokenSource = localTokenSource;

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    action.Invoke(localTokenSource);
                }
                finally
                {
                    if (this.runningTokenSource == localTokenSource)
                    {
                        this.runningTokenSource = null;
                    }

                    localTokenSource.Dispose();
                }

            }, runningTokenSource.Token);
        }

        public Task Run<TContext>(Action<CancellationTokenSource, TContext> action, TContext context)
        {
            if (this.runningTokenSource != null)
            {
                this.runningTokenSource.Cancel();
                this.runningTokenSource = null;
            }

            var localTokenSource = new CancellationTokenSource();

            this.runningTokenSource = localTokenSource;

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    action.Invoke(localTokenSource, context);
                }
                finally
                {
                    if (this.runningTokenSource == localTokenSource)
                    {
                        this.runningTokenSource = null;
                    }

                    localTokenSource.Dispose();
                }

            }, runningTokenSource.Token);
        }


        public static AloneTask Default => SingletonProvider.Get<AloneTask>();
    }
}
