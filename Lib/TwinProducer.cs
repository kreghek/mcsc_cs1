using System.Threading.Tasks;

namespace Lib
{
    public class TwinProducer
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
                    provider.SetValue("bar" + valueCounter.ToString());
                    valueCounter++;
                }
            });
        }
    }
}
