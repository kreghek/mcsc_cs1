using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lib
{
    public class FakeConsumer
    {
        public async Task StartProcessValuesAsync(IValueSource valueSource, CancellationToken ct)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    await valueSource.GetValueAsync("foo");
                }
            }, ct)
                .ContinueWith(task => Console.WriteLine("Consumer stopped"), TaskContinuationOptions.OnlyOnCanceled);
        }
    }
}
