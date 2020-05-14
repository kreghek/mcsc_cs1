using System;
using System.Threading.Tasks;

namespace Lib
{
    public class MegaTimeProducer
    {
        public Task StartGenerateValuesAsync(IValueProvider provider, int durationSec)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    provider.SetValue("baz" + DateTime.Now.Ticks);
                }
            });

            var timeoutTask = Task.Delay(durationSec * 1000);

            return timeoutTask;
        }
    }
}
