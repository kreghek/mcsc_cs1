using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib
{
    public class Consumer
    {
        public async Task StartProcessValuesAsync(IValueSource valueSource, List<string> results)
        {
            var counter = 0;
            await Task.Run(async () =>
            {
                // Защита от бесконечного цикла.
                while (counter <= 1_000_000)
                {
                    var value = await valueSource.GetValueAsync("foo");
                    counter++;
                    results.Add(value);
                }

                // См защиту от бесконечного цикла.
                // Тесты по условиям должны завершаться до этого момент.
                // Если дошло до сюда, значит, вероятнее всего, ошибка в реализации.
                throw new System.Exception();
            });
        }
    }
}
