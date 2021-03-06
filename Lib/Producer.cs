﻿using System.Threading.Tasks;

namespace Lib
{
    public class Producer
    {
        public async Task StartGenerateValuesAsync(IValueProvider provider, int iterationCount)
        {
            var valueCounter = 0;
            await Task.Run(async () =>
            {
                while (valueCounter < iterationCount)
                {
                    await Task.Delay(1000);
                    provider.SetValue(valueCounter.ToString());
                    valueCounter++;
                }
            });
        }
    }
}
